﻿using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using Flow;
public class TableFlow : IFlowTask
{
    public int layer => 0;

    public int order => 16;


    public async Task Logic()
    {
        Debug.Log("加载Table ");
        Table.TableLib.InitTable();
        await Task.CompletedTask;
    }
}
