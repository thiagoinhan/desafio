 
 docker build -f ./backend/prod.Dockerfile . -t thiagoinhan/desafio-toro-backend-arm64:1.0.0 --platform linux/arm64
 docker build -f ./frontend/Toro.Accounting.WebUI/prod.Dockerfile . -t thiagoinhan/desafio-toro-frontend-arm64:1.0.0 --platform linux/arm64