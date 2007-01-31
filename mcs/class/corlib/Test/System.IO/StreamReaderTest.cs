// StreamReaderTest.cs - NUnit Test Cases for the SystemIO.StreamReader class
//
// David Brandt (bucky@keystreams.com)
//
// (C) Ximian, Inc.  http://www.ximian.com
// Copyright (C) 2004 Novell (http://www.novell.com)
// 

using System;
using System.IO;
using System.Text;

using NUnit.Framework;

namespace MonoTests.System.IO
{
[TestFixture]
public class StreamReaderTest
{
	static string TempFolder = Path.Combine (Path.GetTempPath (), "MonoTests.System.IO.Tests");
	private string _codeFileName = TempFolder + Path.DirectorySeparatorChar + "AFile.txt";

	[SetUp]
	public void SetUp ()
	{	
		if (!Directory.Exists (TempFolder))
			Directory.CreateDirectory (TempFolder);
		
		if (!File.Exists (_codeFileName))
			File.Create (_codeFileName).Close ();
	}

	[TearDown]
	public void TearDown ()
	{
		if (Directory.Exists (TempFolder))
			Directory.Delete (TempFolder, true);
	}


	[Test]
	public void TestCtor1() {
		{
			bool errorThrown = false;
			try {
				new StreamReader((Stream)null);
			} catch (ArgumentNullException) {
				errorThrown = true;
			}
			Assert.IsTrue (errorThrown, "null string error not thrown");
		}
		{
			bool errorThrown = false;
			FileStream f = new FileStream(_codeFileName, FileMode.Open, FileAccess.Write);
			try {
				StreamReader r = new StreamReader(f);
				r.Close();
			} catch (ArgumentException) {
				errorThrown = true;
			}
			f.Close();
			Assert.IsTrue (errorThrown, "no read error not thrown");
		}
		{
			// this is probably incestuous, but, oh well.
			FileStream f = new FileStream(_codeFileName, 
						      FileMode.Open, 
						      FileAccess.Read);
			StreamReader r = new StreamReader(f);
			Assert.IsNotNull (r, "no stream reader");
			r.Close();
			f.Close();
		}
	}

	[Test]
	public void TestCtor2() {
		{
			bool errorThrown = false;
			try {
				new StreamReader("");
			} catch (ArgumentException) {
				errorThrown = true;
			} catch (Exception e) {
				Assert.Fail ("Incorrect exception thrown at 1: " + e.ToString());
			}
			Assert.IsTrue (errorThrown, "empty string error not thrown");
		}
		{
			bool errorThrown = false;
			try {
				new StreamReader((string)null);
			} catch (ArgumentNullException) {
				errorThrown = true;
			} catch (Exception e) {
				Assert.Fail ("Incorrect exception thrown at 2: " + e.ToString());
			}
			Assert.IsTrue (errorThrown, "null string error not thrown");
		}
		{
			bool errorThrown = false;
			try {
				new StreamReader("nonexistentfile");
			} catch (FileNotFoundException) {
				errorThrown = true;
			} catch (Exception e) {
				Assert.Fail ("Incorrect exception thrown at 3: " + e.ToString());
			}
			Assert.IsTrue (errorThrown, "fileNotFound error not thrown");
		}
		{
			bool errorThrown = false;
			try {
				new StreamReader("nonexistentdir/file");
			} catch (DirectoryNotFoundException) {
				errorThrown = true;
			} catch (Exception e) {
				Assert.Fail ("Incorrect exception thrown at 4: " + e.ToString());
			}
			Assert.IsTrue (errorThrown, "dirNotFound error not thrown");
		}
		{
			bool errorThrown = false;
			try {
				new StreamReader("!$what? what? Huh? !$*#" + Path.InvalidPathChars[0]);
			} catch (IOException) {
				errorThrown = true;
			} catch (ArgumentException) {
				// FIXME - the spec says 'IOExc', but the
				//   compiler says 'ArgExc'...
				errorThrown = true;
			} catch (Exception e) {
				Assert.Fail ("Incorrect exception thrown at 5: " + e.ToString());
			}
			Assert.IsTrue (errorThrown, "invalid filename error not thrown");
		}
		{
			// this is probably incestuous, but, oh well.
			StreamReader r = new StreamReader(_codeFileName);
			Assert.IsNotNull (r, "no stream reader");
			r.Close();
		}
	}

	[Test]
	public void TestCtor3() {
		{
			bool errorThrown = false;
			try {
				new StreamReader((Stream)null, false);
			} catch (ArgumentNullException) {
				errorThrown = true;
			} catch (Exception e) {
				Assert.Fail ("Incorrect exception thrown at 1: " + e.ToString());
			}
			Assert.IsTrue (errorThrown, "null stream error not thrown");
		}
		{
			bool errorThrown = false;
			FileStream f = new FileStream(_codeFileName, FileMode.Open, FileAccess.Write);
			try {
				StreamReader r = new StreamReader(f, false);
				r.Close();
			} catch (ArgumentException) {
				errorThrown = true;
			} catch (Exception e) {
				Assert.Fail ("Incorrect exception thrown at 2: " + e.ToString());
			}
			f.Close();
			Assert.IsTrue (errorThrown, "no read error not thrown");
		}
		{
			// this is probably incestuous, but, oh well.
			FileStream f = new FileStream(_codeFileName, 
						      FileMode.Open, 
						      FileAccess.Read);
			StreamReader r = new StreamReader(f, false);
			Assert.IsNotNull (r, "no stream reader");
			r.Close();
			f.Close();
		}
		{
			bool errorThrown = false;
			try {
				StreamReader r = new StreamReader((Stream)null, true);
			} catch (ArgumentNullException) {
				errorThrown = true;
			} catch (Exception e) {
				Assert.Fail ("Incorrect exception thrown at 3: " + e.ToString());
			}
			Assert.IsTrue (errorThrown, "null string error not thrown");
		}
		{
			bool errorThrown = false;
			FileStream f = new FileStream(_codeFileName, FileMode.Open, FileAccess.Write);
			try {
				StreamReader r = new StreamReader(f, true);
				r.Close();
			} catch (ArgumentException) {
				errorThrown = true;
			} catch (Exception e) {
				Assert.Fail ("Incorrect exception thrown at 4: " + e.ToString());
			}
			f.Close();
			Assert.IsTrue (errorThrown, "no read error not thrown");
		}
		{
			// this is probably incestuous, but, oh well.
			FileStream f = new FileStream(_codeFileName, 
						      FileMode.Open, 
						      FileAccess.Read);
			StreamReader r = new StreamReader(f, true);
			Assert.IsNotNull (r, "no stream reader");
			r.Close();
			f.Close();
		}
	}

	[Test]
	public void TestCtor4() {
		{
			bool errorThrown = false;
			try {
				new StreamReader("", false);
			} catch (ArgumentException) {
				errorThrown = true;
			} catch (Exception e) {
				Assert.Fail ("Incorrect exception thrown at 1: " + e.ToString());
			}
			Assert.IsTrue (errorThrown, "empty string error not thrown");
		}
		{
			bool errorThrown = false;
			try {
				new StreamReader((string)null, false);
			} catch (ArgumentNullException) {
				errorThrown = true;
			} catch (Exception e) {
				Assert.Fail ("Incorrect exception thrown at 2: " + e.ToString());
			}
			Assert.IsTrue (errorThrown, "null string error not thrown");
		}
		{
			bool errorThrown = false;
			try {
				new StreamReader(TempFolder + "/nonexistentfile", false);
			} catch (FileNotFoundException) {
				errorThrown = true;
			} catch (Exception e) {
				Assert.Fail ("Incorrect exception thrown at 3: " + e.ToString());
			}
			Assert.IsTrue (errorThrown, "fileNotFound error not thrown");
		}
		{
			bool errorThrown = false;
			try {
				new StreamReader(TempFolder + "/nonexistentdir/file", false);
			} catch (DirectoryNotFoundException) {
				errorThrown = true;
			} catch (Exception e) {
				Assert.Fail ("Incorrect exception thrown at 4: " + e.ToString());
			}
			Assert.IsTrue (errorThrown, "dirNotFound error not thrown");
		}
		{
			bool errorThrown = false;
			try {
				new StreamReader("!$what? what? Huh? !$*#" + Path.InvalidPathChars[0], false);
			} catch (IOException) {
				errorThrown = true;
			} catch (ArgumentException) {
				// FIXME - the spec says 'IOExc', but the
				//   compiler says 'ArgExc'...
				errorThrown = true;
			} catch (Exception e) {
				Assert.Fail ("Incorrect exception thrown at 5: " + e.ToString());
			}
			Assert.IsTrue (errorThrown, "invalid filename error not thrown");
		}
		{
			// this is probably incestuous, but, oh well.
			StreamReader r = new StreamReader(_codeFileName, false);
			Assert.IsNotNull (r, "no stream reader");
			r.Close();
		}
		{
			bool errorThrown = false;
			try {
				new StreamReader("", true);
			} catch (ArgumentException) {
				errorThrown = true;
			} catch (Exception e) {
				Assert.Fail ("Incorrect exception thrown at 6: " + e.ToString());
			}
			Assert.IsTrue (errorThrown, "empty string error not thrown");
		}
		{
			bool errorThrown = false;
			try {
				new StreamReader((string)null, true);
			} catch (ArgumentNullException) {
				errorThrown = true;
			} catch (Exception e) {
				Assert.Fail ("Incorrect exception thrown at 7: " + e.ToString());
			}
			Assert.IsTrue (errorThrown, "null string error not thrown");
		}
		{
			bool errorThrown = false;
			try {
				new StreamReader(TempFolder + "/nonexistentfile", true);
			} catch (FileNotFoundException) {
				errorThrown = true;
			} catch (Exception e) {
				Assert.Fail ("Incorrect exception thrown at 8: " + e.ToString());
			}
			Assert.IsTrue (errorThrown, "fileNotFound error not thrown");
		}
		{
			bool errorThrown = false;
			try {
				new StreamReader(TempFolder + "/nonexistentdir/file", true);
			} catch (DirectoryNotFoundException) {
				errorThrown = true;
			} catch (Exception e) {
				Assert.Fail ("Incorrect exception thrown at 9: " + e.ToString());
			}
			Assert.IsTrue (errorThrown, "dirNotFound error not thrown");
		}
		{
			bool errorThrown = false;
			try {
				new StreamReader("!$what? what? Huh? !$*#" + Path.InvalidPathChars[0], true);
			} catch (IOException) {
				errorThrown = true;
			} catch (ArgumentException) {
				// FIXME - the spec says 'IOExc', but the
				//   compiler says 'ArgExc'...
				errorThrown = true;
			} catch (Exception e) {
				Assert.Fail ("Incorrect exception thrown at 10: " + e.ToString());
			}
			Assert.IsTrue (errorThrown, "invalid filename error not thrown");
		}
		{
			// this is probably incestuous, but, oh well.
			StreamReader r = new StreamReader(_codeFileName, true);
			Assert.IsNotNull (r, "no stream reader");
			r.Close();
		}
	}

	// TODO - Ctor with Encoding
	
	[Test]
	public void TestBaseStream() {
		Byte[] b = {};
		MemoryStream m = new MemoryStream(b);
		StreamReader r = new StreamReader(m);
		Assert.AreSame (m, r.BaseStream, "wrong base stream ");
		r.Close();
		m.Close();
	}

	public void TestCurrentEncoding() {
		Byte[] b = {};
		MemoryStream m = new MemoryStream(b);
		StreamReader r = new StreamReader(m);
		Assert.AreEqual (Encoding.UTF8.GetType (), r.CurrentEncoding.GetType (),
			"wrong encoding");
	}

	// TODO - Close - annoying spec - won't commit to any exceptions. How to test?
	// TODO - DiscardBufferedData - I have no clue how to test this function.

	[Test]
	public void TestPeek() {
		// FIXME - how to get an IO Exception?
		Byte [] b;
		MemoryStream m;
		StreamReader r;

		try {
			b = new byte [0];
			m = new MemoryStream (b);
			r = new StreamReader(m);
			m.Close();
			int nothing = r.Peek();
			Assert.Fail ("#1");
		} catch (ObjectDisposedException) {
		}

		b = new byte [] {1, 2, 3, 4, 5, 6};
		m = new MemoryStream (b);
		r = new StreamReader(m);
		for (int i = 1; i <= 6; i++) {
			Assert.AreEqual (i, r.Peek(), "#2");
			r.Read();
		}
		Assert.AreEqual (-1, r.Peek(), "#3");
	}

	[Test]
	public void TestRead() {
		// FIXME - how to get an IO Exception?
		{
			bool errorThrown = false;
			try {
				Byte[] b = {};
				MemoryStream m = new MemoryStream(b);
				StreamReader r = new StreamReader(m);
				m.Close();
				int nothing = r.Read();
			} catch (ObjectDisposedException) {
				errorThrown = true;
			} catch (Exception e) {
				Assert.Fail ("Incorrect exception thrown at 1: " + e.ToString());
			}
			Assert.IsTrue (errorThrown, "nothing-to-read error not thrown");
		}
		{
			Byte[] b = {1, 2, 3, 4, 5, 6};
			MemoryStream m = new MemoryStream(b);
			
			StreamReader r = new StreamReader(m);
			for (int i = 1; i <= 6; i++) {
				Assert.AreEqual (i, r.Read (), "read incorrect");
			}
			Assert.AreEqual (-1, r.Read (), "Should be none left");
		}

		{
			bool errorThrown = false;
			try {
				Byte[] b = {};
				StreamReader r = new StreamReader(new MemoryStream(b));
				r.Read(null, 0, 0);
			} catch (ArgumentNullException) {
				errorThrown = true;
			} catch (ArgumentException) {
				errorThrown = true;
			} catch (Exception e) {
				Assert.Fail ("Incorrect exception thrown at 2: " + e.ToString());
			}
			Assert.IsTrue (errorThrown, "null buffer error not thrown");
		}
		{
			bool errorThrown = false;
			try {
				Byte[] b = {};
				StreamReader r = new StreamReader(new MemoryStream(b));
				Char[] c = new Char[1];
				r.Read(c, 0, 2);
			} catch (ArgumentException) {
				errorThrown = true;
			} catch (Exception e) {
				Assert.Fail ("Incorrect exception thrown at 3: " + e.ToString());
			}
			Assert.IsTrue (errorThrown, "too-long range error not thrown");
		}
		{
			bool errorThrown = false;
			try {
				Byte[] b = {};
				StreamReader r = new StreamReader(new MemoryStream(b));
				Char[] c = new Char[1];
				r.Read(c, -1, 2);
			} catch (ArgumentOutOfRangeException) {
				errorThrown = true;
			} catch (Exception e) {
				Assert.Fail ("Incorrect exception thrown at 4: " + e.ToString());
			}
			Assert.IsTrue (errorThrown, "out of range error not thrown");
		}
		{
			bool errorThrown = false;
			try {
				Byte[] b = {};
				StreamReader r = new StreamReader(new MemoryStream(b));
				Char[] c = new Char[1];
				r.Read(c, 0, -1);
			} catch (ArgumentOutOfRangeException) {
				errorThrown = true;
			} catch (Exception e) {
				Assert.Fail ("Incorrect exception thrown at 5: " + e.ToString());
			}
			Assert.IsTrue (errorThrown, "out of range error not thrown");
		}
		{
			int ii = 1;
			try {
				Byte[] b = {(byte)'a', (byte)'b', (byte)'c', 
					    (byte)'d', (byte)'e', (byte)'f', 
					    (byte)'g'};
				MemoryStream m = new MemoryStream(b);
				ii++;
				StreamReader r = new StreamReader(m);
				ii++;

				char[] buffer = new Char[7];
				ii++;
				char[] target = {'g','d','e','f','b','c','a'};
				ii++;
				r.Read(buffer, 6, 1);
				ii++;
				r.Read(buffer, 4, 2);
				ii++;
				r.Read(buffer, 1, 3);
				ii++;
				r.Read(buffer, 0, 1);
				ii++;
				for (int i = 0; i < target.Length; i++) {
					Assert.AreEqual (target[i], buffer[i], "read no work");
				i++;
				}
			} catch (Exception e) {
				Assert.Fail ("Caught when ii=" + ii + ". e:" + e.ToString());
			}
		}
	}

	[Test]
	public void TestReadLine() {
		// TODO Out Of Memory Exc? IO Exc?
		Byte[] b = new Byte[8];
		b[0] = (byte)'a';
		b[1] = (byte)'\n';
		b[2] = (byte)'b';
		b[3] = (byte)'\n';
		b[4] = (byte)'c';
		b[5] = (byte)'\n';
		b[6] = (byte)'d';
		b[7] = (byte)'\n';
		MemoryStream m = new MemoryStream(b);
		StreamReader r = new StreamReader(m);
		Assert.AreEqual ("a", r.ReadLine(), "#1");
		Assert.AreEqual ("b", r.ReadLine (), "#2");
		Assert.AreEqual ("c", r.ReadLine (), "#3");
		Assert.AreEqual ("d", r.ReadLine(), "#4");
		Assert.IsNull (r.ReadLine (), "#5");
	}

	[Test]
	public void ReadLine1() {
		Byte[] b = new Byte[10];
		b[0] = (byte)'a';
		b[1] = (byte)'\r';
		b[2] = (byte)'b';
		b[3] = (byte)'\n';
		b[4] = (byte)'c';
		b[5] = (byte)'\n';
		b[5] = (byte)'\r';
		b[6] = (byte)'d';
		b[7] = (byte)'\n';
		b[8] = (byte)'\r';
		b[9] = (byte)'\n';
		MemoryStream m = new MemoryStream(b);
		StreamReader r = new StreamReader(m);
		Assert.AreEqual ("a", r.ReadLine (), "#1");
		Assert.AreEqual ("b", r.ReadLine (), "#2");
		Assert.AreEqual ("c", r.ReadLine (), "#3");
		Assert.AreEqual ("d", r.ReadLine (), "#4");
		Assert.AreEqual (string.Empty, r.ReadLine (), "#5");
		Assert.IsNull (r.ReadLine(), "#6");
	}

	[Test]
	public void ReadLine2() {
		Byte[] b = new Byte[10];
		b[0] = (byte)'\r';
		b[1] = (byte)'\r';
		b[2] = (byte)'\n';
		b[3] = (byte)'\n';
		b[4] = (byte)'c';
		b[5] = (byte)'\n';
		b[5] = (byte)'\r';
		b[6] = (byte)'d';
		b[7] = (byte)'\n';
		b[8] = (byte)'\r';
		b[9] = (byte)'\n';
		MemoryStream m = new MemoryStream(b);
		StreamReader r = new StreamReader(m);
		Assert.AreEqual (string.Empty, r.ReadLine (), "#1");
		Assert.AreEqual (string.Empty, r.ReadLine (), "#2");
		Assert.AreEqual (string.Empty, r.ReadLine (), "#3");
		Assert.AreEqual ("c", r.ReadLine (), "#4");
		Assert.AreEqual ("d", r.ReadLine (), "#5");
		Assert.AreEqual (string.Empty, r.ReadLine (), "#6");
		Assert.IsNull (r.ReadLine (), "#7");
	}

	[Test]
	public void ReadLine3() {
		StringBuilder sb = new StringBuilder ();
		sb.Append (new string ('1', 32767));
		sb.Append ('\r');
		sb.Append ('\n');
		sb.Append ("Hola\n");
		byte [] bytes = Encoding.Default.GetBytes (sb.ToString ());
		MemoryStream m = new MemoryStream(bytes);
		StreamReader r = new StreamReader(m);
		Assert.AreEqual (new string ('1', 32767), r.ReadLine(), "#1");
		Assert.AreEqual ("Hola", r.ReadLine (), "#2");
		Assert.IsNull (r.ReadLine (), "#3");
	}

	[Test]
	public void ReadLine4() {
		StringBuilder sb = new StringBuilder ();
		sb.Append (new string ('1', 32767));
		sb.Append ('\r');
		sb.Append ('\n');
		sb.Append ("Hola\n");
		sb.Append (sb.ToString ());
		byte [] bytes = Encoding.Default.GetBytes (sb.ToString ());
		MemoryStream m = new MemoryStream(bytes);
		StreamReader r = new StreamReader(m);
		Assert.AreEqual (new string ('1', 32767), r.ReadLine (), "#1");
		Assert.AreEqual ("Hola", r.ReadLine (), "#2");
		Assert.AreEqual (new string ('1', 32767), r.ReadLine (), "#3");
		Assert.AreEqual ("Hola", r.ReadLine (), "#4");
		Assert.IsNull (r.ReadLine (), "#5");
	}

	[Test]
	public void ReadLine5() {
		StringBuilder sb = new StringBuilder ();
		sb.Append (new string ('1', 32768));
		sb.Append ('\r');
		sb.Append ('\n');
		sb.Append ("Hola\n");
		byte [] bytes = Encoding.Default.GetBytes (sb.ToString ());
		MemoryStream m = new MemoryStream(bytes);
		StreamReader r = new StreamReader(m);
		Assert.AreEqual (new string ('1', 32768), r.ReadLine (), "#1");
		Assert.AreEqual ("Hola", r.ReadLine (), "#2");
		Assert.IsNull (r.ReadLine (), "#3");
	}

	public void TestReadToEnd() {
		// TODO Out Of Memory Exc? IO Exc?
		Byte[] b = new Byte[8];
		b[0] = (byte)'a';
		b[1] = (byte)'\n';
		b[2] = (byte)'b';
		b[3] = (byte)'\n';
		b[4] = (byte)'c';
		b[5] = (byte)'\n';
		b[6] = (byte)'d';
		b[7] = (byte)'\n';
		MemoryStream m = new MemoryStream(b);
		StreamReader r = new StreamReader(m);
		Assert.AreEqual ("a\nb\nc\nd\n", r.ReadToEnd (), "#1");
		Assert.AreEqual (string.Empty, r.ReadToEnd (), "#2");
	}

	[Test]
	public void TestBaseStreamClosed ()
	{
		byte [] b = {};
		MemoryStream m = new MemoryStream (b);
		StreamReader r = new StreamReader (m);
		m.Close ();
		try {
			r.Peek ();
			Assert.Fail ();
		} catch (ObjectDisposedException) {
		}
	}

	[Test]
	[ExpectedException (typeof (ArgumentNullException))]
	public void Contructor_Stream_NullEncoding () 
	{
		new StreamReader (new MemoryStream (), null);
	}

	[Test]
	[ExpectedException (typeof (ArgumentNullException))]
	public void Contructor_Path_NullEncoding () 
	{
		new StreamReader (_codeFileName, null);
	}

	[Test]
	[ExpectedException (typeof (ArgumentNullException))]
	public void Read_Null () 
	{
		StreamReader r = new StreamReader (new MemoryStream ());
		r.Read (null, 0, 0);
	}

	[Test]
	[ExpectedException (typeof (ArgumentException))]
	public void Read_IndexOverflow () 
	{
		char[] array = new char [16];
		StreamReader r = new StreamReader (new MemoryStream (16));
		r.Read (array, 1, Int32.MaxValue);
	}	

	[Test]
	[ExpectedException (typeof (ArgumentException))]
	public void Read_CountOverflow () 
	{
		char[] array = new char [16];
		StreamReader r = new StreamReader (new MemoryStream (16));
		r.Read (array, Int32.MaxValue, 1);
	}

	[Test]
	public void Read_DoesntStopAtLineEndings ()
	{
		MemoryStream ms = new MemoryStream (Encoding.ASCII.GetBytes ("Line1\rLine2\r\nLine3\nLine4"));
		StreamReader reader = new StreamReader (ms);
		Assert.AreEqual (24, reader.Read (new char[24], 0, 24));
	}

	[Test]
	public void bug75526 ()
	{
		StreamReader sr = new StreamReader (new Bug75526Stream ());
		int len = sr.Read (new char [10], 0, 10);
		Assert.AreEqual (2, len);
	}

	class Bug75526Stream : MemoryStream
	{
		public override int Read (byte [] buffer, int offset, int count)
		{
			buffer [offset + 0] = (byte) 'a';
			buffer [offset + 1] = (byte) 'b';
			return 2;
		}
	}
}
}
