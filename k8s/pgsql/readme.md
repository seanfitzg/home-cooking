https://sweetcode.io/how-to-deploy-postgresql-instance-to-kubernetes/

Deploy the PV:
    kubectl apply -f db-persistent-volume.yaml
Deploy the PVC
    kubectl apply -f db-volume-claim.yaml
The environment variables are needed by the cluster. Deploy them as follows:
    kubectl apply -f db-configmap.yaml
Next, create the deployment and add pods replicas.
    kubectl apply -f db-deployment.yaml
Finaly run the service to expose the cluster
    kubectl apply -f db-service.yaml


kubectl apply -f db-persistent-volume.yaml & kubectl apply -f db-volume-claim.yaml & kubectl apply -f db-configmap.yaml & kubectl apply -f db-deployment.yaml & kubectl apply -f db-service.yaml