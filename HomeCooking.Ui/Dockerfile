# pull official base image
FROM node:13.12.0-alpine

# set working directory
WORKDIR /app

# add `/app/node_modules/.bin` to $PATH
ENV PATH /app/node_modules/.bin:$PATH

# install app dependencies
COPY package.json ./
COPY package-lock.json ./
RUN npm install --silent
RUN npm install react-scripts@3.4.1 -g --silent

# add app
COPY . ./

# start app
CMD ["npm", "start"]


# https://mherman.org/blog/dockerizing-a-react-app/

# docker build -t homecooking-ui:dev .

# docker run -it --rm -v ${PWD}:/app -v /app/node_modules -p 3000:3000 -e CHOKIDAR_USEPOLLING=true homecooking-ui:dev

#or windows:
# docker run -it --rm -v %cd%:/app -v /app/node_modules -p 3000:3000 -e CHOKIDAR_USEPOLLING=true homecooking-ui:dev

# to run without docker:
# dapr run --app-id homecooking-ui --app-port 80 yarn start

# from root folder:
# docker build -f ./HomeCooking.Ui/Dockerfile -t homecooking-ui:dev ./HomeCooking.Ui