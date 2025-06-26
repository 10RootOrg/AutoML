import pandas as pd
import numpy as np
from sklearn.tree import DecisionTreeRegressor
from sklearn.preprocessing import StandardScaler
from sklearn.metrics import silhouette_score
from sklearn.cluster import KMeans
import os
import joblib
import matplotlib.pyplot as plt
import seaborn as sns
from utils.config_loader import config

def train_and_save_model(data, processed_data, logger):
    try:
        # Use original data for clustering
        logger.info("Using original data for clustering.")
        original_data = data.copy()

        # Feature Importance via Synthetic Target
        initial_model = DecisionTreeRegressor(random_state=42)
        scaler = StandardScaler()
        X_scaled = scaler.fit_transform(processed_data)
        synthetic_target = np.sum(X_scaled, axis=1)
        initial_model.fit(X_scaled, synthetic_target)

        feature_importances = pd.DataFrame({
            'Feature': processed_data.columns,
            'Importance': initial_model.feature_importances_
        }).sort_values(by='Importance', ascending=False)

        # Select the most important features
        min_features = min(8, len(processed_data.columns))
        cumulative_importance = feature_importances['Importance'].cumsum()
        n_features = max(min_features, len(feature_importances[cumulative_importance <= 0.95]))
        important_features = feature_importances['Feature'].head(n_features).tolist()
        logger.info(f"Selected {len(important_features)} important features out of {len(processed_data.columns)} total features.")
        logger.info("Important features: " + ", ".join(important_features))

        # Use the important features for clustering
        processed_data_important = processed_data[important_features]
        X_scaled = scaler.fit_transform(processed_data_important)

        # Retrieve hyperparameters from config
        n_clusters = int(getattr(config, 'LIMIT_CLUSTERS_NUMBER', 2))
        init = config.MODEL_HYPERPARAMETERS['KMeans']['init']
        max_iter = int(config.MODEL_HYPERPARAMETERS['KMeans']['max_iter'])
        tol = config.MODEL_HYPERPARAMETERS['KMeans']['tol']

        # Validate hyperparameters
        if not isinstance(n_clusters, int) or n_clusters <= 0:
            logger.warning("Invalid n_clusters. Using default value: 8.")
            n_clusters = 2
        if init not in ['k-means++', 'random']:
            logger.warning("Invalid init method. Using default value: 'k-means++'.")
            init = 'k-means++'
        if not isinstance(max_iter, int) or max_iter <= 0:
            logger.warning("Invalid max_iter. Using default value: 300.")
            max_iter = 300
        if not isinstance(tol, (float, int)) or tol <= 0:
            logger.warning("Invalid tol. Using default value: 1e-4.")
            tol = 1e-4

        # KMeans Clustering
        logger.info(f"Clustering with KMeans into {n_clusters} clusters...")
        kmeans = KMeans(n_clusters=n_clusters, random_state=42, n_init=10, init=init, max_iter=max_iter, tol=tol)
        kmeans.fit(X_scaled)
        leaf_assignments = kmeans.labels_

        actual_n_clusters = len(np.unique(leaf_assignments))
        logger.info(f"Final number of clusters: {actual_n_clusters}")

        # Calculate Silhouette Score
        if actual_n_clusters > 1:
            silhouette_avg = silhouette_score(X_scaled, leaf_assignments)
            logger.info(f"Silhouette Score: {silhouette_avg:.2f}")

        # Add cluster assignments to the original dataset
        original_data['Cluster'] = leaf_assignments
        os.makedirs(config.MODEL_EVALUATIONS_FOLDER_PATH, exist_ok=True)

        # Cluster Distribution
        plt.figure(figsize=(12, 6))
        cluster_sizes = original_data['Cluster'].value_counts().sort_index()
        sns.barplot(x=cluster_sizes.index, y=cluster_sizes.values)
        plt.title(f'Cluster Size Distribution (Total {actual_n_clusters} clusters)')
        plt.xlabel('Cluster')
        plt.ylabel('Number of Samples')

        for i, v in enumerate(cluster_sizes.values):
            plt.text(i, v, str(v), ha='center', va='bottom')

        cluster_dist_path = os.path.join(config.MODEL_EVALUATIONS_FOLDER_PATH, "cluster_distribution.png")
        plt.savefig(cluster_dist_path)
        plt.close()

        # Final Feature Importances
        final_importances = feature_importances[feature_importances['Feature'].isin(important_features)]
        logger.info("\nFinal Feature Importances (Based on Initial Decision Tree):")
        logger.info(final_importances.to_string(index=False))

        plt.figure(figsize=(12, 8))
        sns.barplot(x=final_importances['Importance'], y=final_importances['Feature'], color='skyblue')
        plt.xlabel('Importance')
        plt.ylabel('Feature')
        plt.title('Feature Importances (Selected Features)')
        plt.tight_layout()
        plot_path = os.path.join(config.MODEL_EVALUATIONS_FOLDER_PATH, "feature_importances.png")
        plt.savefig(plot_path)
        plt.close()

        # Cluster Characteristics
        cluster_stats = processed_data_important.groupby(original_data['Cluster']).agg(['mean', 'std']).round(2)
        cluster_stats_path = os.path.join(config.MODEL_EVALUATIONS_FOLDER_PATH, "cluster_statistics.csv")
        cluster_stats.to_csv(cluster_stats_path)

        for cluster in range(actual_n_clusters):
            cluster_data = original_data[original_data['Cluster'] == cluster]
            logger.info(f"\nCluster {cluster} Statistics:")
            logger.info(f"Size: {len(cluster_data)} samples ({len(cluster_data)/len(original_data)*100:.1f}% of total)")

        # Save model and metadata
        os.makedirs(os.path.dirname(config.SAVE_MODEL_FILE_PATH), exist_ok=True)
        model_data = {
            'model': kmeans,
            'scaler': scaler,
            'n_clusters': actual_n_clusters,
            'important_features': important_features
        }
        joblib.dump(model_data, config.SAVE_MODEL_FILE_PATH)
        logger.info(f"Model and metadata saved to {config.SAVE_MODEL_FILE_PATH}")

        # Save clustered data to output path in config
        clustered_data_path = config.OUTPUT_FILE_PATH
        os.makedirs(os.path.dirname(clustered_data_path), exist_ok=True)
        original_data.to_csv(clustered_data_path, index=False)
        logger.info(f"Clustered data saved to {clustered_data_path}")

        return kmeans, scaler

    except Exception as e:
        logger.error(f"Error during model training and saving: {e}", exc_info=True)
        raise
