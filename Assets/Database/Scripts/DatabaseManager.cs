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
            //Table users
            "CREATE TABLE IF NOT EXISTS users(" +
            "`id_user` INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT," +
            "`username` VARCHAR(32) NOT NULL," +
            "`password` VARCHAR(32) NOT NULL," +
            "`wallet` FLOAT NOT NULL);" +
            //Table items
            "CREATE TABLE IF NOT EXISTS items(" +
            "`id_item` INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT," +
            "`item` VARCHAR(32) NOT NULL," +
            "`image` VARCHAR(32) NOT NULL," +
            "`price` INTEGER NOT NULL," +
            "`description` TEXT);" +
            //Table weapons
            "CREATE TABLE IF NOT EXISTS weapons(" +
            "`id_weapon` INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT," +
            "`weapon` VARCHAR(32) NOT NULL," +
            "`image` VARCHAR(32) NOT NULL," +
            "`price` INTEGER NOT NULL," +
            "`description` TEXT);" +
            //Table armours
            "CREATE TABLE IF NOT EXISTS armours(" +
            "`id_armour` INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT," +
            "`armour` VARCHAR(32) NOT NULL," +
            "`image` VARCHAR(32) NOT NULL," +
            "`price` INTEGER NOT NULL," +
            "`description` TEXT);" +
            //Table users_items
            "CREATE TABLE IF NOT EXISTS users_items(" +
            "`id_user_item` INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT," +
            "`id_user` INTEGER NOT NULL," +
            "`id_item` INTEGER NOT NULL," +
            "FOREIGN KEY (id_user) REFERENCES users(id_user)," +
            "FOREIGN KEY (id_item) REFERENCES items(id_item));" +
            //Table users_weapons
            "CREATE TABLE IF NOT EXISTS users_weapons(" +
            "`id_user_weapon` INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT," +
            "`id_user` INTEGER NOT NULL," +
            "`id_weapon` INTEGER NOT NULL," +
            "FOREIGN KEY (id_user) REFERENCES users(id_user)," +
            "FOREIGN KEY (id_weapon) REFERENCES weapons(id_weapon));" +
            //Table users_armours
            "CREATE TABLE IF NOT EXISTS users_armours(" +
            "`id_user_armour` INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT," +
            "`id_user` INTEGER NOT NULL," +
            "`id_armour` INTEGER NOT NULL," +
            "FOREIGN KEY (id_user) REFERENCES users(id_user)," +
            "FOREIGN KEY (id_armour) REFERENCES armours(id_armour));";

        createTablesCommand.CommandText = queryCreateTables;
        createTablesCommand.ExecuteNonQuery();
        dbcon.Close();
    }

}
