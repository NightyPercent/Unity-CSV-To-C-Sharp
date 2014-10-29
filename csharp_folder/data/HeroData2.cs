﻿using UnityEngine;
using System.Collections;

public class HeroData2 : csvDataParent{

	private volatile static HeroData2 _instance = null;
    private static readonly object lockHelper = new object();
    
	public static HeroData2 Instance()
	{
        if(_instance == null)
        {
            lock(lockHelper)
            {
                if(_instance == null)
                     _instance = new HeroData2();
            }
        }
        return _instance;
    }

	//"等级|血量|魔法"
	private const string _csvAllKey = "level|hp|mp";
	
	// {"10","12","11"}
	private string[] _csvDataArray = {
		"1|10|10",
		"2|20|30",
		"3|30|30",
		"4|50|40",
		"5|90|100",
		
	};




	// "key1","key2","key3"
	private string[] _AllKey = null;

	// {"10","12","11"}
	private string[][] _DataArray = null;

	private HeroData2(){
		_AllKey = _csvAllKey.Split('|');

		int dataNum = _csvDataArray.Length;

		_DataArray = new string[dataNum][];

		for(int i=0; i<dataNum; i++)
		{
			_DataArray[i] = _csvDataArray[i].Split('|');
		}
	}

	//打印
	public override void print()
	{

		int row = _DataArray.Length;
		int column = _DataArray[0].Length;

		for(int i=0; i<row; i++)
		{
			string printData = i+"row: ";
			for(int j=0; j<column; j++)
			{
				printData+=(_DataArray[i][j]+" ");
			}
			Debuger.Log(printData);
		}

	}

	//获取所有的Key
	public override string[] getKeyArray()
	{
		return _AllKey;
	}

	//获取所有的Data
	public override string[][] getDataArray()
	{
		return _DataArray;
	}

	public  override int num()
	{
		return _DataArray.Length;
	}

	public  override int keynum()
	{
		return _AllKey.Length;
	}

	//通过type获取num标识 
	private int getTypeNum(string typeName)
	{
		for(int i=0; i<keynum(); i++)
		{
			if(_AllKey[i] == typeName)
			{
				return i;
			}
		}
		return -1;
	}

	//输入数据查询是否出现过，并且返回Key。（查到第一个直接返回）
	public override string getKeyFromData(string _data)
	{
		int row = _DataArray.Length;
		int column = _DataArray[0].Length;
		
		for(int i=0; i<row; i++)
		{
			for(int j=0; j<column; j++)
			{
				if(_DataArray[i][j].Equals(_data))
				{
					return _AllKey[j];
				}
			}
		}

		return "";
	}

	//通过类型和行数获取内容 num 0 start
	public override string get(int num,string typeName)
	{
		int typenum = getTypeNum(typeName);
		if(typenum==-1)
		{
			Debuger.Log(typeName+"   "+num+"  error");
			return "-1";
		}
		return _DataArray[num][typenum];
	}
	
	//转换get的类型为int返回
	public override int getInt(int num,string typeName)
	{
		string strItm = get(num,typeName);

		return int.Parse(strItm);
	}

	//转换get的类型为float返回
	public override float getFloat(int num,string typeName)
	{
		string strItm = get(num,typeName);

		return float.Parse(strItm);
	}
}