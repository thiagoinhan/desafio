FROM node:alpine AS angular-build
WORKDIR /app
COPY . .
RUN npm ci && npm run build

FROM nginx:alpine
COPY --from=angular-build /app/dist/accounting /usr/share/nginx/html
COPY /nginx/default.conf /etc/nginx/conf.d/
EXPOSE 80