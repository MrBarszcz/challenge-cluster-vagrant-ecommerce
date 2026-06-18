echo "=== Iniciando o Swarm no Master ==="
sudo docker swarm init --advertise-addr=192.168.56.11

echo "=== Gerando o script de join para os workers ==="
sudo docker swarm join-token worker | grep docker > /vagrant/worker.sh