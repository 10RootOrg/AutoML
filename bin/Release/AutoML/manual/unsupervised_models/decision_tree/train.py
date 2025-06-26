import pandas as pd
import numpy as np
from sklearn.tree import DecisionTreeRegressor
from sklearn.preprocessing import StandardScaler
import os
import joblib
import matplotlib.pyplot as plt
import seaborn as sns
from utils.config_loader import config

def train_and_save_model(data, processed_data, logger):
    try:
        # Feature importance using synthetic target
        initial_model = DecisionTreeRegressor(random_state=42)
        scaler = StandardScaler()
        X_scaled = scaler.fit_transform(processed_data)
        synthetic_target = np.sum(X_scaled, axis=1)
        initial_model.fit(X_scaled, synthetic_target)

        # Calculate feature importances
        feature_importances = pd.DataFrame({
            'Feature': processed_data.columns,
            'Importance': initial_model.feature_importances_
        }).sort_values(by='Importance', ascending=False)

        # Feature selection
        min_features = min(8, len(processed_data.columns))
        cumulative_importance = feature_importances['Importance'].cumsum()
        n_features = max(
            min_features,
            len(feature_importances[cumulative_importance <= 0.95])
        )
        important_features = feature_importances['Feature'].head(n_features).tolist()
        logger.info(f"Selected {len(important_features)} important features out of {len(processed_data.columns)} total features")
        logger.info("Important features: " + ", ".join(important_features))

        # Prepare selected features
        processed_data_important = processed_data[important_features]
        X_scaled = scaler.fit_transform(processed_data_important)

        # Configure clustering hyperparameters
        n_clusters = getattr(config, 'LIMIT_CLUSTERS_NUMBER', 5)
        if not isinstance(n_clusters, int) or n_clusters <= 0:
            logger.warning("Invalid or missing 'LIMIT_CLUSTERS_NUMBER'. Using default value: 5.")
            n_clusters = 5

        min_samples_leaf = max(1, len(processed_data) // (n_clusters * 10))

        # Perform binary search for optimal max_leaf_nodes
        min_leaf = n_clusters
        max_leaf = n_clusters * 2
        best_model = None
        best_assignments = None

        while min_leaf <= max_leaf:
            current_leaf = (min_leaf + max_leaf) // 2
            temp_model = DecisionTreeRegressor(random_state=42, max_leaf_nodes=current_leaf, min_samples_leaf=min_samples_leaf)
            temp_model.fit(X_scaled, synthetic_target)
            temp_assignments = temp_model.apply(X_scaled)
            unique_clusters = len(np.unique(temp_assignments))

            logger.info(f"Tried max_leaf_nodes={current_leaf}, got {unique_clusters} clusters")
            if unique_clusters == n_clusters:
                best_model = temp_model
                best_assignments = temp_assignments
                break
            elif unique_clusters < n_clusters:
                min_leaf = current_leaf + 1
            else:
                max_leaf = current_leaf - 1

        if best_model is None:
            logger.warning(f"Could not achieve exactly {n_clusters} clusters. Using closest match.")
            best_model = temp_model
            best_assignments = temp_assignments

        # Finalize cluster assignments
        model = best_model
        leaf_assignments = best_assignments
        unique_clusters = np.unique(leaf_assignments)
        cluster_map = {old: new for new, old in enumerate(unique_clusters)}
        leaf_assignments = np.array([cluster_map[x] for x in leaf_assignments])

        actual_n_clusters = len(np.unique(leaf_assignments))
        logger.info(f"Final number of clusters: {actual_n_clusters}")

        # Add cluster assignments back to the real dataset (data)
        data['Cluster'] = leaf_assignments

        # Save cluster distribution plot
        os.makedirs(config.MODEL_EVALUATIONS_FOLDER_PATH, exist_ok=True)
        plt.figure(figsize=(12, 6))
        cluster_sizes = data['Cluster'].value_counts().sort_index()
        sns.barplot(x=cluster_sizes.index, y=cluster_sizes.values)
        plt.title(f'Cluster Size Distribution (Total {actual_n_clusters} clusters)')
        plt.xlabel('Cluster')
        plt.ylabel('Number of Samples')
        for i, v in enumerate(cluster_sizes.values):
            plt.text(i, v, str(v), ha='center', va='bottom')
        cluster_dist_path = os.path.join(config.MODEL_EVALUATIONS_FOLDER_PATH, "cluster_distribution.png")
        plt.savefig(cluster_dist_path)
        plt.close()

        # Save clustered real data to output folder in config
        clustered_data_path = config.OUTPUT_FILE_PATH
        os.makedirs(os.path.dirname(clustered_data_path), exist_ok=True)
        data.to_csv(clustered_data_path, index=False)
        logger.info(f"Clustered data saved to {clustered_data_path}")

        # Save model and metadata
        model_data = {
            'model': model,
            'scaler': scaler,
            'n_clusters': actual_n_clusters,
            'cluster_map': cluster_map,
            'important_features': important_features
        }
        os.makedirs(os.path.dirname(config.SAVE_MODEL_FILE_PATH), exist_ok=True)
        joblib.dump(model_data, config.SAVE_MODEL_FILE_PATH)
        logger.info(f"Model and metadata saved to {config.SAVE_MODEL_FILE_PATH}")

        return model, scaler

    except Exception as e:
        logger.error(f"Error during model training and saving: {e}", exc_info=True)
        raise
