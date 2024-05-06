// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityServer4;
using IdentityServer4.Models;
using System.Collections.Generic;
using System.Linq;

namespace Multishop.IdentityServer
{
    public static class Config
    {
        public static IEnumerable<ApiResource> ApiResources => new ApiResource[]
        {
            new ApiResource("ResourceCatalog") { Scopes = {"CatalogFullPermission","CatalogReadPermission"}},
            new ApiResource("ResourceDiscount"){ Scopes = {"DiscountFullPermission"}},
            new ApiResource("ResourceOrder")   { Scopes = {"OrderFullPermission"}},

            new ApiResource("ResourceCargo"){Scopes={"CargoFullPermission"}},

            new ApiResource("ResourceBasket"){Scopes={"BasketFullPermission"}},

            new ApiResource(IdentityServerConstants.LocalApi.ScopeName)
        };

        public static IEnumerable<IdentityResource> IdentityResources => new IdentityResource[]
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Email(),
            new IdentityResources.Profile(),
        };

        public static IEnumerable<ApiScope> ApiScopes => new ApiScope[]
        {
            new ApiScope("CatalogFullPermission","Full auth for catalog operations"),
            new ApiScope("CatalogReadPermission","Reading auth for catalog operations"),
            new ApiScope("DiscountFullPermission","Full auth for discount operations"),
            new ApiScope("OrderFullPermission","Full auth for order operations"),

            new ApiScope("CargoFullPermission","Full auth for cargo operations"),
            
            new ApiScope("BasketFullPermission","Full auth for basket operations"),

            new ApiScope(IdentityServerConstants.LocalApi.ScopeName)
        };

        public static IEnumerable<Client> Clients => new Client[]
        {
            //visitor
            new Client
            {
                ClientId = "MultiShopVisitorId",
                ClientName = "MultiShop Client User",
                AllowedGrantTypes = GrantTypes.ClientCredentials,
                ClientSecrets = { new Secret("multishopsecret".Sha256()) },
                AllowedScopes = { "DiscountFullPermission" }
            },

            //manager
            new Client
            {
                ClientId="MultiShopManagerId",
                ClientName="MultiShop Manager User",
                AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                ClientSecrets = { new Secret("multishopsecret".Sha256()) },
                AllowedScopes = { "CatalogReadPermission", "CatalogFullPermission","BasketFullPermission",
                    IdentityServerConstants.LocalApi.ScopeName }
            },

            //admin
             new Client
            {
                ClientId="MultiShopAdminId",
                ClientName="MultiShop Admin User",
                AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                ClientSecrets = { new Secret("multishopsecret".Sha256()) },
                AllowedScopes = { 
                     "CatalogReadPermission", "CatalogFullPermission", "DiscountFullPermission", "OrderFullPermission", "CargoFullPermission","BasketFullPermission",
                  IdentityServerConstants.LocalApi.ScopeName,
                  IdentityServerConstants.StandardScopes.Email,
                  IdentityServerConstants.StandardScopes.Profile,
                  IdentityServerConstants.StandardScopes.OpenId
                }, AccessTokenLifetime=600
            },
        };
    }
}
