terraform {
  backend "azurerm" {
    resource_group_name  = "rg-tfstate"
    storage_account_name = "st4jh38kpjowq7"
    container_name       = "tfstate"
    key                  = "dev.discord.tfstate"
  }
}

provider "azurerm" {
  version = "=2.30.0"
  features {}
}

data "terraform_remote_state" "shared_resources" {
  backend = "azurerm"
  config = {
    resource_group_name  = "rg-tfstate"
    storage_account_name = "st4jh38kpjowq7"
    container_name       = "tfstate"
    key                  = "dev.shared.tfstate"
  }
}

resource "azurerm_app_service" "app" {
  name                = "app-discord-dev"
  resource_group_name = data.terraform_remote_state.shared_resources.outputs.az_shared_rg_name
  location            = data.terraform_remote_state.shared_resources.outputs.az_location
  app_service_plan_id = data.terraform_remote_state.shared_resources.outputs.az_plan_free_id
  
  # site_config {
  #   always_on = true
  # }

  app_settings = {
    "DISCORD_BOT_TOKEN"     = var.discord_bot_token
  }
}