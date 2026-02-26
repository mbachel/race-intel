#!/bin/sh
set -e

CERT_DIR="/etc/nginx/certs"
CERT="$CERT_DIR/cert.pem"
KEY="$CERT_DIR/key.pem"

if [ ! -f "$CERT" ] || [ ! -f "$KEY" ]; then
    echo "[start.sh] No TLS certs found - generating temporary self-signed cert..."
    mkdir -p "$CERT_DIR"
    openssl req -x509 -nodes -days 365 -newkey rsa:2048 \
        -keyout "$KEY" -out "$CERT" \
        -out "$CERT" \
        -subj "/CN=localhost"
    echo "[start.sh] Self-signed cert generated."
fi

echo "[start.sh] Starting Next.js server..."
node /app/server.js &

echo "[start.sh] Starting nginx..."
exec nginx -g "daemon off;"