provider "azurerm" {
    version = "2.5.0"
    features {}
}

resource "azurerm_resource_group" "gunkut_dev" {
    name = "itemtrader-tf"
    location = "westus2"
}

resource "azurerm_app_service_plan" "gunkut_dev" {
    name = "itemtrader-appserviceplan"
    location = "westus2"
    resource_group_name = "itemtrader-tf"

    sku {
        tier = "Free"
        size = "S0"
    }
}

resource "azurerm_app_service" "gunkut_dev" {
    name = "itemtrader-appservice"
    location = azurerm_resource_group.gunkut_dev.location
    resource_group_name = azurerm_resource_group.gunkut_dev.name
    app_service_plan_id = azurerm_app_service_plan.gunkut_dev.id    
}



