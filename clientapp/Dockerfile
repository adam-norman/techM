#############
### build ###
#############

# base image
FROM node:14 as build

# set working directory
WORKDIR /app

# add `/app/node_modules/.bin` to $PATH
ENV PATH /app/node_modules/.bin:$PATH

# install and cache app dependencies
COPY package.json /app/package.json
COPY package-lock.json /app/package-lock.json

RUN npm install

RUN npm install -g @angular/cli@14.1.2


# add app
COPY . /app

# generate build
RUN ng build --prod --output-path=dist

############
### prod ###
############

# base image
FROM nginx:1.21.6

# copy artifact build from the 'build environment'
COPY --from=build /app/dist /usr/share/nginx/html

# expose port 80
EXPOSE 80

# run nginx
# CMD ["nginx", "-g", "daemon off;"]
