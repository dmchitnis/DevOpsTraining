FROM node:alpine AS builder

WORKDIR /app

COPY . .

#RUN ["chmod", "+x", "./config.sh"]
#CMD ./config.sh

RUN npm install && \
    npm run build

FROM nginx:alpine
COPY --from=builder /app/dist/* /usr/share/nginx/html/
