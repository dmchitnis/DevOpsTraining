def apiImage
def uiImage
stage('Prepare'){    
    node {
        checkout([$class: 'GitSCM', branches: [[name: '*/master']], doGenerateSubmoduleConfigurations: false, extensions: [], submoduleCfg: [], userRemoteConfigs: [[url: 'https://github.com/dmchitnis/DevOpsTraining.git']]])
        echo 'Build Number:' + env.BUILD_NUMBER
        sh label: 'Versions of Tools', script: '''
        node -v
        npm -v
        dotnet --version
        ng version
        '''
        sh label: 'Credentials', script: '''
        gcloud auth configure-docker
        gcloud container clusters get-credentials my-cluster --zone us-east1-b --project scenic-comfort-253917
        '''
    }
}
stage('Run tests'){
    node {
        dir('Test'){
        sh label: 'run dotnet test', script: '''
        dotnet test
        '''        
        }
    node {
        dir('myapi'){
            //echo $scannerHome
            //cd $scannerHome
            //def scannerHome = tool 'sonardotnet';
            //withSonarQubeEnv(installationName: 'sonarqube-1') {
                sh label: 'SonarQube Scan', script: '''
                dotnet tool install --global dotnet-sonarscanner
                dotnet sonarscanner begin /k:"dotnet-myapi" 
                dotnet build
                dotnet sonarscanner end
                '''
                //}
            }
        }
    }
}
stage('Build and push Docker images'){
    node {
        dir('myapi'){
        apiImage = docker.build("gcr.io/scenic-comfort-253917/myapi:v0.${env.BUILD_NUMBER}")
        apiImage.push()
        sh 'docker image rm gcr.io/scenic-comfort-253917/myapi:v0.$BUILD_NUMBER --force'
        }
        dir('UI'){
        uiImage = docker.build("gcr.io/scenic-comfort-253917/myui:v0.${env.BUILD_NUMBER}")
        uiImage.push()
        sh 'docker image rm gcr.io/scenic-comfort-253917/myui:v0.$BUILD_NUMBER --force'
        }
    }
}
stage('Deploy to Kubernetes'){
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
stage('Cleanup'){
    node {
    }
}
