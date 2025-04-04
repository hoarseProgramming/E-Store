﻿namespace EStore.Api;

public static class ApiEndpoints
{
    private const string ApiBase = "api";

    public static class Products
    {
        private const string Base = $"{ApiBase}/products";

        public const string Create = Base;

        public const string Get = $"{Base}/{{productNumber:int}}";

        public const string GetAll = Base;

        public const string Update = $"{Base}/{{productNumber:int}}";

        public const string Delete = $"{Base}/{{productNumber:int}}";
    }

    public static class Customers
    {
        private const string Base = $"{ApiBase}/customers";

        public const string Create = Base;

        public const string Get = $"{Base}/{{id:Guid}}";

        public const string GetAll = Base;

        public const string Update = $"{Base}/{{id:Guid}}";

        public const string Delete = $"{Base}/{{id:Guid}}";
    }

    public static class Categories
    {
        private const string Base = $"{ApiBase}/categories";

        public const string Create = Base;

        public const string Get = $"{Base}/{{id:Guid}}";

        public const string GetAll = Base;

        public const string Delete = $"{Base}/{{id:Guid}}";
    }

    public static class Orders
    {
        private const string Base = $"{ApiBase}/orders";

        public const string Create = Base;

        public const string Get = $"{Base}/{{id:Guid}}";

        public const string GetAll = Base;

        public const string Delete = $"{Base}/{{id:Guid}}";
    }

    public static class Auth
    {
        private const string Base = $"{ApiBase}/user";

        public const string Get = $"{Base}/info";

        public const string SetCustomerId = $"{Base}/set-customer";

        public const string UpdateUserAndCustomer = $"{Base}/{{id:Guid}}";

        //public const string RegisterAdmin = $"{Base}/register-admin";

    }
}