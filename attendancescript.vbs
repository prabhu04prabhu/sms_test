Call LogEntry()
 
Sub LogEntry()
 
        On Error Resume Next
 
        Dim objRequest
        Dim URL
 
        Set objRequest = CreateObject("Microsoft.XMLHTTP")
        URL = "http://localhost:21387/frmAttendanceLogFinalAuto.aspx?mobileview=yes&id=1008"
 
        objRequest.open "POST", URL , false
 
        objRequest.Send
 
        Set objRequest = Nothing
End Sub