pipeline {
    agent any
    stages {
        stage('Build and Deploy') {
            steps {
                sh '''cd PatenPottery
                    docker compose build ppottery_app
                    docker compose up -d
                    '''
            }
        }
    }
}