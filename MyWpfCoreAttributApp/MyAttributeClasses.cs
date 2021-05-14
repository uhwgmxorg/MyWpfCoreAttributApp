using System;

namespace MyWpfCoreAttributApp
{
    public class MyAttribute1 : Attribute
    {
    }

    public class MyAttribute11 : Attribute
    {
    }

    public class MyAttribute2 : Attribute
    {
    }

    public class MyAttribute22 : Attribute
    {
    }

    public class MyAttribute3 : Attribute
    {
    }

    public class MyAttribute33 : Attribute
    {
    }

    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class MyPermissionAttribute : Attribute
    {
        public string Permission { get; }

        public MyPermissionAttribute(string permission)
        {
            Permission = permission;
        }
    }
}
