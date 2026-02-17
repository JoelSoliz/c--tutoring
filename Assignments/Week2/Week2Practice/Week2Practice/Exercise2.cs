using System;
using System.Collections.Generic;

public interface IRepository<T> 
{ 
    void Add(T item);
    IEnumerable<T> GetAll(); // a collection that can be iterable
}

public class InMemoryRepository<T> : IRepository<T> 
{
    private List<T> genericList;

    public InMemoryRepository() 
    {
        genericList = new List<T>();
    }

    public void Add(T item) 
    { 
        genericList.Add(item);
    }

    public IEnumerable<T> GetAll()
    { 
        return genericList;
    }
}

public class Character
{ 
    public string Name { get; set; }
    public string Description { get; set; }

    public Character(string name, string description) 
    {
        Name = name;
        Description = description;
    }
}

