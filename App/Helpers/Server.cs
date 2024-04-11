using System.Net;

public class Server
{

	public static HttpListenerRequest StartServer(string[] prefixes)
	{

		HttpListener listener = new HttpListener();
		if (prefixes == null || prefixes.Length == 0)
			prefixes = ["http://localhost:8080/"];

		foreach (string s in prefixes)
		{
			listener.Prefixes.Add(s);
		}

		listener.Start();
		Display.PrintSuccessMessage("Waiting for authentication code...");
		// Note: The GetContext method blocks while waiting for a request.
		HttpListenerContext context = listener.GetContext();
		HttpListenerRequest request = context.Request;
		HttpListenerResponse response = context.Response;
		string responseString = "<HTML><BODY> Successfully authenticated!</BODY></HTML>";
		byte[] buffer = System.Text.Encoding.UTF8.GetBytes(responseString);
		response.ContentLength64 = buffer.Length;
		System.IO.Stream output = response.OutputStream;
		output.Write(buffer, 0, buffer.Length);
		output.Close();
		listener.Stop();
		return request;
	}
}