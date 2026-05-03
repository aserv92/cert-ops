#!/usr/bin/env bash

set -euo pipefail

docker.ensure.network.exists() {
  local network_name="${1}"

  docker network ls | grep "${network_name}" > /dev/null \
  || docker network create "${network_name}"
}

docker.get.host.user.id() {
  if [ "$(uname)" == 'Darwin' ]; then
    echo 1000
  else
    id -u
  fi
}

docker.get.tty.flag() {
  if [ -t 0 ]; then
    echo "-t"
  fi
}
