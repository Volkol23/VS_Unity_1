using UnityEngine;

using System.Collections;
using System.Collections.Generic;

using System.Data;
using Mono.Data.Sqlite;
using System.IO;
using System;

public class DatabaseManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        CreateDatabase();

        //Metodos para crear de nuevo la base de datos para debug

        //DropTables();
        //CreateDatabaseTables();
        //InsertAllItems();
        //InsertUser("ola", "k ase", UnityEngine.Random.Range(5000, 20000));
    }

    private void CreateDatabase()
    {
        //if (CheckIfExists() != 0)
        {
            CreateDatabaseTables();
            //InsertAllItems();
        }
    }
    private void CreateDatabaseTables()
    {
        string connection = "URI=file:" + Application.persistentDataPath + "/RoguePirate.sqlite";

        IDbConnection dbcon = new SqliteConnection(connection);
        dbcon.Open();

        IDbCommand createTablesCommand; 
        createTablesCommand = dbcon.CreateCommand();
        string queryCreateTables =
            //Table melee_weapons
            "CREATE TABLE IF NOT EXISTS melee_weapons(" +
            "`id_melee_weapon` INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT," +
            "`melee_weapon` VARCHAR(32) NOT NULL," +
            "`base_damage` FLOAT NOT NULL," +
            "`wallet` FLOAT NOT NULL);" +
            //Table ranged_weapons
            "CREATE TABLE IF NOT EXISTS items(" +
            "`id_item` INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT," +
            "`item` VARCHAR(32) NOT NULL," +
            "`image` VARCHAR(32) NOT NULL," +
            "`price` INTEGER NOT NULL," +
            "`description` TEXT);";

        createTablesCommand.CommandText = queryCreateTables;
        createTablesCommand.ExecuteNonQuery();
        dbcon.Close();
    }

}
