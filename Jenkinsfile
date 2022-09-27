pipeline {
    agent any
    environment {
        dotnet = 'C:\\Program Files\\dotnet\\dotnet.exe'
    }
    stages {
        stage('Checkout Stage') {
            steps {
                git credentialsId: '20f0k253-05fd-4l8e-9310-85587adba70d', url: 'https://github.com/dhanarthi/EducationPortalAPI.git', branch: 'master'
            }
        }
        stage('Build Stage') {
            steps {
                bat 'C:\\ProgramData\\Jenkins\\.jenkins\\workspace\\TestPipeline\\EducationPortalAPI.sln --configuration Release'
            }
        }
        stage('Test Stage') {
            steps {
                bat 'dotnet test %WORKSPACE%\\EducationPortalAPI.csproj'
            }
        }
        stage("Release Stage") {
            steps {
                bat 'dotnet build %WORKSPACE%\\EducationPortalAPI.csproj /p:PublishProfile=" %WORKSPACE%\\EducationPortalAPI\\Properties\\PublishProfiles\\FolderProfile.pubxml" /p:Platform="Any CPU" /p:DeployOnBuild=true /m'
            }
        }
        stage('Deploy Stage') {
            steps {
                //Deploy application on IIS
                bat 'net stop "w3svc"'
                bat '"C:\\Program Files (x86)\\IIS\\Microsoft Web Deploy V3\\msdeploy.exe" -verb:sync -source:package="%WORKSPACE%\\EducationPortalAPI\\bin\\Debug\\netcoreapp2.1\\EducationPortalAPI.zip" -dest:auto -setParam:"IIS Web Application Name"="Demo.Web" -skip:objectName=filePath,absolutePath=".\\\\PackagDemoeTmp\\\\web.config$" -enableRule:DoNotDelete -allowUntrusted=true'
                bat 'net start "w3svc"'
            }
        }
    }
}
