stage('Checkout Code'){    
    node {
        checkout([$class: 'GitSCM', branches: [[name: '*/master']], doGenerateSubmoduleConfigurations: false, extensions: [], submoduleCfg: [], userRemoteConfigs: [[url: 'https://github.com/dmchitnis/DevOpsTraining.git']]])
    }
}
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
stage('Build and run automated tests'){
    node {
        dir('Test'){
        sh label: 'run dotnet test', script: '''
        ls -a
        dotnet test
        '''
        }
    }
}
stage('Build and push docker images'){
    node {
        dir('myapi'){
        def apiImage = docker.build("gcr.io/scenic-comfort-253917/myapi:v0.${env.BUILD_NUMBER}")
        apiImage.push()
        }
        dir('UI'){
        def uiImage = docker.build("gcr.io/scenic-comfort-253917/myui:v0.${env.BUILD_NUMBER}")
        uiImage.push()
        }
    }
}
stage('Deploy to Kebernetes'){
    node{
        dir('deploy'){
            sh label: 'Run scripts for deploying to Kubernetes', script: '''
            sed -i 's/BUILDNUMBER/'"$BUILD_NUMBER"'/g'  deploy-api.yaml 
            sed -i 's/BUILDNUMBER/'"$BUILD_NUMBER"'/g'  deploy-ui.yaml
            cat deploy-api.yaml 
            /usr/local/bin/kubectl apply -f deploy-api.yaml
            cat deploy-ui.yaml
            /usr/local/bin/kubectl apply -f deploy-ui.yaml
            /usr/local/bin/kubectl get deployments
            '''
        }
    }
}
stage('Cleanup Docker images on Build server'){
    node {
        sh 'docker rmi $(docker images -q) -f'
    }
}