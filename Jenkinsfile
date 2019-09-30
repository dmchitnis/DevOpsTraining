stage('Validate tools exist'){
    node {
        echo 'Build Number:' + env.BUILD_NUMBER
        sh label: 'Versions of Tools', script: '''
        node -v
        npm -v
        dotnet --version
        ng version
        '''
    }
}
stage('Setup Credentials'){
    node {
        sh label: 'Credentials', script: '''
        gcloud auth configure-docker
        gcloud container clusters get-credentials my-cluster --zone us-east1-b --project scenic-comfort-253917
        '''
    }
}

