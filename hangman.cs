using System;
using System.Collections.Generic;
using System.Threading;

namespace hm_test
{
	class MainClass
	{
		/// <summary>
		/// The progress of the hanging tree [_null, head, body, arm, arm2, leg, leg2] 
		/// </summary>
		public enum hangingTreeType
		{
			_null,
			head,
			body,
			arm,
			arm2,
			leg,
			leg2
		}

		/// <summary>
		/// badLetters
		/// </summary>
		public static LinkedList<char> usedBadLetters = new LinkedList<char> ();
		/// <summary>
		/// the index for the hanging tree definition
		/// </summary>
		/// <value>The body.</value>
		public static int body { get; set; }
		/// <summary>
		/// the word for the game selected by the player
		/// </summary>
		public static string word = "";
		public static hangingTreeType cur_htt = hangingTreeType._null;
		public static bool play = true;
		public static char[] spaces { get; set; }

		public static void Main (string[] args)
		{
			word = getSecretWord ("Word:] ");
			spaces = new char[word.Length];
			for (int i = 0; i < word.Length; i++) {
				spaces [i] = '_';
			}
			draw (cur_htt, spaces, usedBadLetters);


			///Main Loop
			while (play) {
				draw (cur_htt, spaces, usedBadLetters);
				getInputForGame ();
				if (body == 0)
					cur_htt = hangingTreeType._null;
				else if (body == 1)
					cur_htt = hangingTreeType.head;
				else if (body == 2)
					cur_htt = hangingTreeType.body;
				else if (body == 3)
					cur_htt = hangingTreeType.arm;
				else if (body == 4)
					cur_htt = hangingTreeType.arm2;
				else if (body == 5)
					cur_htt = hangingTreeType.leg;
				else if (body == 6)
					cur_htt = hangingTreeType.leg2;
				else
					play = false;
			}

			Console.Clear ();
			Console.WriteLine ("Sorry, you lost");
			Console.WriteLine ("The word was {0}", word);
		}

		/// <summary>
		/// Returns teh hanging tree drawn as a string
		/// </summary>
		/// <returns>The hanging tree from type.</returns>
		/// <param name="htt">The definitions for the hanging tree</param>
		public static string getHangingTreeFromType(hangingTreeType htt)
		{
			#region 

			if (htt == hangingTreeType._null) {
				return "   -----\n   |   |\n   |   \n   |   \n   |   \n   |   \n_______";
			} else if (htt == hangingTreeType.head) {
				return "   -----\n   |   |\n   |   +\n   |   \n   |   \n   |   \n_______";
			} else if (htt == hangingTreeType.body) {
				return "   -----\n   |   |\n   |   +\n   |   * \n   |   *\n   |   \n_______";
			} else if (htt == hangingTreeType.arm) {
				return "   -----\n   |   |\n   |   +\n   |  =* \n   |   *\n   |   \n_______";
			} else if (htt == hangingTreeType.arm2) {
				return "   -----\n   |   |\n   |   +\n   |  =*= \n   |   *\n   |   \n_______";
			} else if (htt == hangingTreeType.leg) {
				return "   -----\n   |   |\n   |   +\n   |  =*= \n   |  /*\n   |   \n_______";
			} else if (htt == hangingTreeType.leg2) {
				return "   -----\n   |   |\n   |   +\n   |  =*= \n   |  /*\\\n   |   \n_______";
			}
			else
				return null;

			#endregion
		}

		public static bool isCharValid(string keys, char testChar)
		{
			foreach (char thisChar in keys) {
				if (thisChar == testChar)
					return true;
			}
			return false;
		}

		public static char[] updateWordSpaces(char[] listToUpdate, char updateChar)
		{
			char[] key_List = new char[word.Length];
			int index = 0;
			foreach (char thisChar in word) {
				key_List [index] = thisChar;
				index++;
			}
			for (int i = 0; i < listToUpdate.Length; i++) {
				if (key_List [i] == updateChar) {
					listToUpdate [i] = key_List [i];
				}
			}
			return listToUpdate;

		}

		public static void getInputForGame()
		{
			Console.Write("\nPICK A LETTER: ");
			ConsoleKeyInfo cki_tmp = Console.ReadKey (false);
			if (isCharValid (word, cki_tmp.KeyChar)) {
				updateWordSpaces (spaces, cki_tmp.KeyChar);
			} else {
				usedBadLetters.AddLast(cki_tmp.KeyChar);
				body++;
			}
		}

		public static string getSecretWord(string prompt)
		{
			string tmp_word = "";
			Console.Write (prompt);
			bool tmp_b = false;
			while (!tmp_b) {
				ConsoleKeyInfo cki = Console.ReadKey (true);
				if (cki.Key == ConsoleKey.Enter)
					tmp_b = true;
				else
					tmp_word += cki.KeyChar.ToString ();
			}
			Console.Clear ();
			return tmp_word;
		}

		public static void updateLine(char[] chars)
		{
			Console.WriteLine ();
			foreach (char thisChar in chars) {
				Console.Write (thisChar.ToString ());
			}
			Console.WriteLine ();
		}



		/// <summary>
		/// draws the gui
		/// </summary>
		/// <param name="current_htt">The Hanging Tree Type to use</param>
		/// <param name="letters">The array to populate the word, or lack thereof</param>.</param>
		/// <param name="badLetters">Bad letters</param>
		public static void draw(hangingTreeType current_htt, char[] letters, LinkedList<char> badLetters)
		{
			Console.Clear ();
			Console.WriteLine ("HANGMAN [V1.0]");
			Console.WriteLine ("(c) Holewinski Studios 2017");
			for (int i = 0; i < Console.BufferWidth; i++) {
				Console.Write ("=");
			}
			Console.WriteLine ();

			Console.WriteLine ("\n\n");

			Console.WriteLine (getHangingTreeFromType (cur_htt));
			Console.WriteLine ();
			Console.WriteLine ("WRONG LETTERS");
			if (badLetters.Count == 0) {
				Console.WriteLine("NO BAD LETTERS... YET");
			}
			else {
				foreach (char thisBadChar in badLetters) {
					Console.Write (thisBadChar.ToString ());
				}
				Console.WriteLine ();
			}

			Console.WriteLine ("\n");

			updateLine (letters);
		}
	}
}
