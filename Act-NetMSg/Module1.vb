Imports System.Net
Imports System.Net.Sockets
Imports System.Text


Module Server
    Const LISTEN_PORT As Integer = 8902

    Private udp_Listener As UdpClient
    Private done As Boolean = False

    Private Declare Auto Function ShowWindow Lib "user32.dll" (ByVal hWnd As IntPtr, ByVal nCmdShow As Integer) As Boolean
    Private Declare Auto Function GetConsoleWindow Lib "kernel32.dll" () As IntPtr
    Private Const SW_HIDE As Integer = 0


    Sub Main()
        Dim hWndConsole As IntPtr
        hWndConsole = GetConsoleWindow()
        ShowWindow(hWndConsole, SW_HIDE)
        start_Listener()

    End Sub

    Private Sub start_Listener()
        udp_Listener = New UdpClient(LISTEN_PORT)
        Dim msg_End_Point As New IPEndPoint(IPAddress.Any, LISTEN_PORT)

        Try
            While Not done
                Console.WriteLine("Waiting for broadcast")
                Dim bytes As Byte() = udp_Listener.Receive(msg_End_Point)
                Console.WriteLine("Received broadcast from {0} :", msg_End_Point.ToString())
                Dim message As String = Encoding.ASCII.GetString(bytes, 0, bytes.Length)
                If message = "EndServer Auth 8902" Then
                    Exit While
                End If
                MsgBox(message)
                Console.WriteLine()
            End While
        Catch e As Exception
            Console.WriteLine(e.ToString())
        Finally
            udp_Listener.Close()
        End Try
    End Sub

End Module
