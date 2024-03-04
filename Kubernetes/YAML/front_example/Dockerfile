# Use the official Node image as a base image
FROM node:latest as builder

# Set the working directory inside the container
WORKDIR /app

# Copy package.json and package-lock.json to the working directory
COPY package*.json ./

# Install Angular CLI globally
RUN npm install -g @angular/cli

# Install project dependencies
RUN npm install

# Copy the entire project to the working directory
COPY . .

# Build the Angular project
RUN ng build --configuration production

# Use the official Nginx image as a base image for the production image
FROM nginx:alpine

# Copy the built Angular app to the default Nginx public folder
COPY --from=builder /app/dist/life-copilot /usr/share/nginx/html

# Expose port 9090
EXPOSE 80

# Default command to run Nginx
CMD ["nginx", "-g", "daemon off;"]

