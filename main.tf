terraform{
    required_providers {
        azurerm = {
            source = "hashicorp/azurerm"
        }
        random = {
            source  = "hashicorp/random"
            version = "3.0.1"
        }    
    }

    backend "remote" {
        organization = "gunkut_dev"

        workspaces {
            name = "ItemTrader-APIDriven"
        }
    }
}

provider "azurerm" {
    version = "2.5.0"
    features {}
}

resource "azurerm_resource_group" "gunkut_dev" {
    name = "itemtrader-tf"
    location = "westus2"
}

resource "azurerm_sql_server" "gunkut_dev" {
    name = "itetrader-dbserver"
    resource_group_name = azurerm_resource_group.gunkut_dev.name
    location = azurerm_resource_group.gunkut_dev.location
    version = "12.0"
    administrator_login = "gunkut"
    administrator_login_password = "camurdanEv23$"    
}

resource "azurerm_sql_database" "gunkut_dev" {
    name = "ItemTraderDB"
    resource_group_name = azurerm_resource_group.gunkut_dev.name
    location = azurerm_resource_group.gunkut_dev.location
    server_name = azurerm_sql_server.gunkut_dev.name
}

resource "azurerm_app_service_plan" "gunkut_dev" {
    name = "itemtrader-appserviceplan"
    location = "westus2"
    resource_group_name = "itemtrader-tf"

    sku {
        tier = "Free"
        size = "F0"
    }    
}

resource "azurerm_app_service" "itemtrader_api" {
    name = "itemtrader-appservice"
    location = azurerm_resource_group.gunkut_dev.location
    resource_group_name = azurerm_resource_group.gunkut_dev.name
    app_service_plan_id = azurerm_app_service_plan.gunkut_dev.id   

    connection_string {
        name = "DefaultConnection"
        type = "sqlserver"
        value = "Server=azurerm_sql_server.gunkut_dev.sql_server_fqdn;Database=azurerm_sql_database.gunkut_dev.database_name;uid=gunkut;camurdanEv23$"
    }
}

resource "azurerm_app_service" "auth_server" {
    name = "itemtrader-authserver"
    location = azurerm_resource_group.gunkut_dev.location
    resource_group_name = azurerm_resource_group.gunkut_dev.name
    app_service_plan_id = azurerm_app_service_plan.gunkut_dev.id   

    connection_string {
        name = "DefaultConnection"
        type = "sqlserver"
        value = "Server=azurerm_sql_server.gunkut_dev.sql_server_fqdn;Database=azurerm_sql_database.gunkut_dev.database_name;uid=gunkut;camurdanEv23$"
    }
}

output "app_service_name_itemtrader_api" {
  value = "${azurerm_app_service.itemtrader_api.name}"
}

output "app_service_default_hostname_itemtrader_api" {
  value = "https://${azurerm_app_service.itemtrader_api.default_site_hostname}"
}

output "app_service_name_auth_server" {
  value = "${azurerm_app_service.auth_server.name}"
}

output "app_service_default_hostname_auth_server" {
  value = "https://${azurerm_app_service.auth_server.default_site_hostname}"
}



