# Variables
$imageName = "passtxt"
$containerName = "passtxt1"
$dockerFilePath = "Dockerfile"              # Path to the Dockerfile (assuming it's in the current directory)
$dotnetEnvironment = "development"          # Set the DOTNET_ENVIRONMENT variable

# Function to check if a Docker container exists and is running
function Get-DockerContainer {
    param (
        [string]$containerName
    )
    docker ps -q -f "name=$containerName"
}

# Function to check if a Docker container exists but is stopped
function Get-StoppedDockerContainer {
    param (
        [string]$containerName
    )
    docker ps -a -q -f "name=$containerName"
}

# Function to remove a Docker container if it exists
function Remove-DockerContainer {
    param (
        [string]$containerName
    )

    # Check if the container is running
    $containerId = Get-DockerContainer -containerName $containerName
    if ($containerId) {
        Write-Host "Stopping and removing container: $containerName..."
        docker stop $containerName
        docker rm $containerName
    } else {
        # Check if the container exists but is stopped
        $stoppedContainerId = Get-StoppedDockerContainer -containerName $containerName
        if ($stoppedContainerId) {
            Write-Host "Removing stopped container: $containerName..."
            docker rm $containerName
        } else {
            Write-Host "Container $containerName does not exist."
        }
    }
}

# Remove existing container (keep the image)
Remove-DockerContainer -containerName $containerName

# Rebuild the Docker image (this will reuse layers if possible)
Write-Host "Rebuilding Docker image: $imageName from Dockerfile: $dockerFilePath..."
docker build -t $imageName -f $dockerFilePath .

# Run the container with DOTNET_ENVIRONMENT set to the variable value
Write-Host "Running container with name: $containerName and DOTNET_ENVIRONMENT=$dotnetEnvironment..."

#docker run --name $containerName -e "DOTNET_ENVIRONMENT=$dotnetEnvironment" $imageName
docker run -p 5200:8080 --name $containerName -e "DOTNET_ENVIRONMENT=$dotnetEnvironment" $imageName

Write-Host "Container $containerName is now running with DOTNET_ENVIRONMENT=$dotnetEnvironment."

# -----------------------------------------------------------------------------------------
# MANUAL COMMANDS TO RUN:
# 
# 1. Stop and Remove Existing Container:
#    - These commands stop and remove the container if it is running or exists in a stopped state.
#    Commands:
#      docker ps -q -f "name=passtxt1"  # Check if the container is running.
#      docker stop passtxt1             # Stop the container if it's running.
#      docker rm passtxt1               # Remove the container.
#
# 2. Rebuild the Docker Image:
#    - This builds the Docker image using the specified Dockerfile and tags it as 'passtxt'. Docker will reuse cached layers where possible to speed up the build process.
#    Command:
#      docker build -t passtxt -f Dockerfile .
#
# 3. Run the Docker Container:
#    - This runs the container with the environment variable DOTNET_ENVIRONMENT set to 'development'.
#    Command:
#      docker run --name passtxt1 -e "DOTNET_ENVIRONMENT=development" passtxt
#      docker run -p 5200:8080 --name passtxt1 -e "DOTNET_ENVIRONMENT=development" passtxt
# -----------------------------------------------------------------------------------------
