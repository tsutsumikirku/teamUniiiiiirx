using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test
{
    string name;
    List<float> values;
    public string Name { get => name; }
    public List<float> Values { get => values; }
    public test(string name, List<float> values)
    {
        this.name = name;
        this.values = values;
    }
}
