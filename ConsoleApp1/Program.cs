﻿// ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
// Company:...... J.H. Kelly
// Department:... BIM/VC
// Website:...... http://www.jhkelly.com
// Solution:..... WordpressPostGenerator
// Project:...... ConsoleApp1
// File:......... Program.cs ✓✓
// ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

namespace ConsoleApp1;

using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

using Dapper;

using MySql.Data.MySqlClient;

internal class Program
{

	private static MySqlConnection _connection;

	private static string _database;

	private static string _password;

	private static string _server;

	private static string _uid;

	public static void Insert()
	{
		//using IDbConnection connection = _connection;


		// ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
		// Person Data
		var personName = "Christopher Andreas Hildebran";


		//var personBirthDateTime = new DateTime(1922, 07, 02, 0, 0, 0);
		var personBirthDateTime = new DateTime(1989, 09, 04, 0, 0, 0);

		var personDeathDateIfAlive = personBirthDateTime.AddYears(100);

		var personDeathDateTime = new DateTime(1997, 07, 30, 21, 55, 0);


		// ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
		// Date Math
		var dateTimeOfPost = personBirthDateTime;

		var nowDateTime = DateTime.Now;

		var span = personDeathDateTime.Subtract(personBirthDateTime);

		Console.WriteLine("Time Difference (days): " + span.Days);

		List<WpPost> listOfPosts = new();

		//var lastId = connection.Query<int>("WpPostsGetMaxId").Single();

		for (var i = 0; i < span.Days; i++)
		{
			//lastId++;

			var postContent = " What happened today? ";

			var post = new WpPost
			{
				PostAuthor          = 3,
				PostDate            = dateTimeOfPost,
				PostDateGmt         = dateTimeOfPost,
				PostContent         = postContent,
				PostTitle           = $"{dateTimeOfPost.Year:D4}.{dateTimeOfPost.Month:D2}.{dateTimeOfPost.Day:D2} - {dateTimeOfPost.DayOfWeek} Journal For {personName}",
				PostExcerpt         = string.Empty,
				PostStatus          = "publish",
				CommentStatus       = "open",
				PingStatus          = "open",
				PostPassword        = string.Empty,
				PostName            = $"{dateTimeOfPost.Year:D4}-{dateTimeOfPost.Month:D2}-{dateTimeOfPost.Day:D2}-{dateTimeOfPost.DayOfWeek}-Journal-For-{personName.Replace(' ', '-')}",
				ToPing              = string.Empty,
				Pinged              = string.Empty,
				PostModified        = dateTimeOfPost,
				PostModifiedGmt     = dateTimeOfPost,
				PostContentFiltered = string.Empty,
				PostParent          = 0,
				Guid                = "http://sheasta.com/TimeLine/?p=" ,//+ lastId,
				PostType            = "post",
				PostMimeType        = string.Empty,
				CommentCount        = 0
			};

			listOfPosts.Add(post);

			dateTimeOfPost = dateTimeOfPost.AddDays(1);

			Console.WriteLine(post.PostContent);
		}

		Console.WriteLine("Total Records In List: " + listOfPosts.Count);

		try
		{
			WriteUpdaterSettingsToMarkdown(listOfPosts, "");

			//connection.Execute("WpPostsInsert", listOfPosts, commandType: CommandType.StoredProcedure);
		}
		catch (Exception e)
		{
			Console.WriteLine(e.StackTrace);
		}
	}


	//Close connection
	private static bool CloseConnection()
	{
		try
		{
			_connection.Close();

			return true;
		}
		catch (MySqlException ex)
		{
			Debug.Write(ex.Message);

			return false;
		}
	}

	private static void CreateConnection()
	{
		//_server   = "mysql503.discountasp.net";
		//_database = "MYSQL5_1005625_timeline";
		//_uid      = "timeline";
		//_password = "iPQ8NZzxtAXQ84qejUfv";

		//var connectionString = "SERVER=" + _server + ";" + "DATABASE=" + _database + ";" + "UID=" + _uid + ";" + "PASSWORD=" + _password + ";";

		//_connection = new MySqlConnection(connectionString);

		//var isOpen = OpenConnection();

		//if (isOpen)
		//{
		//	Console.WriteLine("Connection Is Open!");
		//}
		//else
		//{
		//	Console.WriteLine("Connection Failed To Open.");
		//}


		// Prompt To Insert
		Console.WriteLine("Insert Records?");

		var insertRecordsRead = Console.ReadLine();

		if (insertRecordsRead.Equals("Yes", StringComparison.CurrentCultureIgnoreCase))

		{
			Insert();
		}


		// Prompt to Close
	}

	private static void CreateFile(string filePath, StringBuilder stringBuilder)
	{
		try
		{
			using var sw = File.CreateText(filePath);

			sw.WriteLine(stringBuilder.ToString());
		}
		catch (Exception e) { }
	}

	private static void Main(string[] args)
	{
		CreateConnection();
	}

	private static bool OpenConnection()
	{
		try
		{
			_connection.Open();

			return true;
		}
		catch (MySqlException ex)
		{
			//When handling errors, you can your application's response based 
			//on the error number.
			//The two most common error numbers when connecting are as follows:
			//0: Cannot connect to server.
			//1045: Invalid user name and/or password.
			switch (ex.Number)
			{
				case 0:

					Debug.Write("Cannot connect to server.  Contact administrator");

					break;

				case 1045:

					Debug.Write("Invalid username/password, please try again");

					break;
			}

			return false;
		}
	}

	private static void WriteUpdaterSettingsToMarkdown(List<WpPost> posts, string pathModifier)
	{
		if (posts == null)
		{
			return;
		}

		var sb = new StringBuilder();

		foreach (var post in posts)
		{
			var title = $"{post.PostDate}_{post.PostTitle}";


			// Heading 3
			sb.AppendLine($"# {title}");


			// Code Block Style
			sb.AppendLine("Post Name: "    + post.PostName);
			sb.AppendLine("Post Content: " + post.PostContent);

			var path = $"c:\\Markdown\\{title}.md";

			CreateFile(path, sb);
		}
	}

}