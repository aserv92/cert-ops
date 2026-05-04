# [WIP] SSL Certificate Manager for LetsEncrypt

## Description
A CLI toolkit for managing LetsEncrypt SSL certificates.

## Goals
- Provide a CLI tool that defines/modifies configuration for SSL certificate generation
- Provide a daemon that auto updates SSL certificates per the configuration
  - Integrate with web server software (like nginx, maybe others)
  - Integrate with cloud DNS management tools like Route53, Cloudflare, etc
- Release this toolkit for linux, windows, and MacOS

### CLI command Structure
- `--version` -> show version information
- `--help`, `-h` -> show help/info
- `--verbose`, `-v` -> increase verbosity
- `--config`, `-c` \<configuration file> -> set the configuration file to use
- `version` -> show version information
- `help` -> show help
- `config`
  - `help` -> show help
  - `init` -> interactively generate a new config with the user
  - `set` -> cert-ops config set abc.123=xyz
  - `get` -> cert-ops config get abc.123=xyz
- `status` -> show current status
- `renew` -> renew now
- `revoke` -> revoke current certificate

### Daemon command Structure
- `--version`, `-v` -> show version information
- `--help`, `-h` -> show help/info
- `--config`, `-c` \<configuration file> -> set the configuration file to use
