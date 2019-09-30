stage('Validate code is checked out'){
    node {
    dir('Test'){sh 'ls -a'}
    dir('myapi'){sh 'ls -a'}
    dir('UI'){sh 'ls -a'} 
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
