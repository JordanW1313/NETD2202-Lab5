Option Strict On
Imports System.IO

'*******************************************************************************************
'	Name: Jordan Wriker
'	Course: NETD 2202
'	Date: July 29, 2020
'	Program Name: Text Editor
'	Purpose: A simple text editor. Allows user to create and save new text documents, as well
'            as open existing documents. Also allows copy, cut and paste functionality. 
'
'
'*******************************************************************************************
Public Class frmTextEditor
#Region "Variable declaration"
    Dim star As Integer         'Decalres an integer variable called star
    Dim fileChanged As Boolean  'Decalres an boolean variable called fileChanged
    Dim path As String          'Decalres an string variable called path
#End Region

#Region "Event Handlers"
    ''' <summary>
    ''' Event handler that sets the fileChanged variable to true when the 
    ''' text in the text box is changed.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub TxtEditorTextChanged(sender As Object, e As EventArgs) Handles txtEditor.TextChanged
        ' Sets the fileChanged variable to true
        fileChanged = True
        ' increments the star variable by 1
        star += 1

        ' if statement that runs if star is equal to 1
        If star = 1 Then
            ' adds a * to the end of the form title
            Me.Text = Me.Text & "*"
        End If
    End Sub
    ''' <summary>
    ''' Event handler that starts a new text document when the new menu item is clicked
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub MnuNewClick(sender As Object, e As EventArgs) Handles mnuFileNew.Click
        ' calls the ConfirmClose method
        ConfirmClose()
        ' if statement that runs if fileChanged is false
        If fileChanged = False Then
            ' sets the path variable to an empty string
            path = String.Empty
            ' empties the txtEditor text box
            txtEditor.Text = String.Empty
            ' sets the form title to "Text Editor"
            Me.Text = "Text Editor "
            ' sets file changed to false
            fileChanged = False
            ' sets the star variable to 0
            star = 0
        End If
    End Sub
    ''' <summary>
    ''' Event handle that opens a dialog box and allows the user to select
    ''' and open existing text files.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub MnuOpenClick(sender As Object, e As EventArgs) Handles mnuFileOpen.Click
        ' calls the ConfirmClose method
        ConfirmClose()
        ' if statement that runs if the fileChanged variable is false
        If fileChanged = False Then
            ' creates a new OpenFileDialog instance called open
            Dim open As New OpenFileDialog
            ' shows the open file dialog window
            open.ShowDialog()
            ' sets the path variable to the chose file variable
            path = open.FileName
            ' if statement that runs if the path variabel is not empty
            If Not path = String.Empty Then
                ' creates a new instance of FileStream called file
                Dim file As New FileStream(path, FileMode.Open, FileAccess.Read)
                ' creates a new instance of StreamReader called streamRead
                Dim streamRead As New StreamReader(file)
                ' Sets the txtEditor text to the text from the opened file
                txtEditor.Text = streamRead.ReadToEnd()
                ' closes streamRead
                streamRead.Close()
                ' appends the name of the text to the end of the form title
                Me.Text = "Text Editor - " & path
                ' sets the fileChanged variable to false
                fileChanged = False
                ' sets the star variable to 0
                star = 0
            End If
        End If
    End Sub
    ''' <summary>
    ''' Event handler that saves the the current existing file or saves as new file
    ''' if the path variable is empty
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub MnuSaveClick(sender As Object, e As EventArgs) Handles mnuFileSave.Click
        ' calls the Save method
        Save(path)
    End Sub
    ''' <summary>
    ''' Saves the current text as new text files
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub MnuSaveAsClick(sender As Object, e As EventArgs) Handles mnuFileSaveAs.Click
        ' declares a string variable called emptyPath and sets it to empty
        Dim emptyPath As String = String.Empty
        ' calls the Save method
        Save(emptyPath)
    End Sub

    Private Sub MnuCloseClick(sender As Object, e As EventArgs) Handles mnuFileClose.Click
        ' calls the ConfirmClose method
        ConfirmClose()
        ' if statement that runs if the fileChanged variable is false
        If fileChanged = False Then
            ' sets the path variable to empty
            path = String.Empty
            ' set the txtEditor text to empty
            txtEditor.Text = String.Empty
            ' sets the form title to "Text Editor"
            Me.Text = "Text Editor"
            ' sets the fileChanged variable to false
            fileChanged = False
            ' sets the star variable to 0
            star = 0
        End If
    End Sub
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub MnuExitClick(sender As Object, e As EventArgs) Handles mnuFileExit.Click
        ' calls the ConfirmClose method
        ConfirmClose()
        ' if statement that runs if the fileChanged variable is false 
        If fileChanged = False Then
            ' exits the application
            Application.Exit()
        End If
    End Sub
    ''' <summary>
    ''' event handler that copys the selected text from the text box to the clipboard
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub MnuCopyClick(sender As Object, e As EventArgs) Handles mnuEditCopy.Click
        ' clears the clipboard
        Clipboard.Clear()
        ' If statement that runs if ther is selected text in the tex box
        If Not txtEditor.SelectedText = String.Empty Then
            ' copies the selected text from the txtEditor text box to the clipboard
            My.Computer.Clipboard.SetText(txtEditor.SelectedText)
        End If
    End Sub
    ''' <summary>
    ''' event handler that cuts the selected text from the text box to the clipboard
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub mnuCut_Click(sender As Object, e As EventArgs) Handles mnuEditCut.Click
        ' clears the clipboard
        Clipboard.Clear()
        ' If statement that runs if ther is selected text in the tex box
        If Not txtEditor.SelectedText = "" Then
            ' copies the selected text from the txtEditor text box to the clipboard
            My.Computer.Clipboard.SetText(txtEditor.SelectedText)
            ' sets the selected text to an empty string
            txtEditor.SelectedText = String.Empty
        End If
    End Sub
    ''' <summary>
    ''' event handler that pastes the text from the clipboard to the text box
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub mnuPaste_Click(sender As Object, e As EventArgs) Handles mnuEditPaste.Click
        ' sets the selected text from the txtEditor to the text in the clipboard
        txtEditor.SelectedText = My.Computer.Clipboard.GetText()
    End Sub
    ''' <summary>
    ''' Event handler that opens the about window
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub MnuAboutClick(sender As Object, e As EventArgs) Handles mnuHelpAbout.Click
        ' opens a message box with the course code the lab number and my name
        MessageBox.Show("NETD 2202" & vbCrLf & vbCrLf & "Lab #5" & vbCrLf & vbCrLf & "J. Wriker", "About")
    End Sub
#End Region

#Region "Methods"
    ''' <summary>
    ''' Method that confirms if the user wants to close the current text
    ''' without saving
    ''' </summary>
    Sub ConfirmClose()
        ' if statement that runs if the fileChanged variable is true
        If fileChanged = True Then
            ' if statement that runs if the user clicks yes to the pop up window
            If MsgBox("There are unsaved changes to the current file. Are you sure want to close the page?", MsgBoxStyle.YesNo, "Text Editor") = MsgBoxResult.Yes Then
                ' sets the fileChanged variable to false
                fileChanged = False
                ' else statement that runs if the user clicked no on the pop up window
            Else
                ' sets the fileChanged variable to true
                fileChanged = True
            End If
        Else
            ' sets the fileChanged variable to false
            fileChanged = False
        End If
    End Sub
    ''' <summary>
    ''' Method that save existing or new files
    ''' </summary>
    ''' <param name="path"></param>
    Sub Save(path As String)
        ' if statement that runs if the path variable isn't blank
        If Not path = String.Empty Then
            ' New instance of FileStream called file
            Dim file As New FileStream(path, FileMode.Create, FileAccess.Write)
            ' using statement that uses the file object
            Using (file)
                ' new instance of StreamWriter called fileWrite
                Dim fileWrite As New StreamWriter(file)
                ' writes the text from the txtEditor text box to the text file
                fileWrite.Write(txtEditor.Text)
                ' closes fileWrite
                fileWrite.Close()
                ' sets the fileChanged variable to false
                fileChanged = False
                ' appends the file name to the form title
                Me.Text = "Text Editor - " & path
                ' sets the star variable to 0
                star = 0
            End Using
        Else
            Dim saveFile As New SaveFileDialog
            saveFile.Filter = "Text files (*.txt)|*.txt"
            saveFile.DefaultExt = "txt"
            saveFile.ShowDialog()
            path = saveFile.FileName

            If Not path = String.Empty Then
                ' New instance of FileStream called file
                Dim file As New FileStream(path, FileMode.Create, FileAccess.Write)
                ' using statement that uses the file object
                Using (file)
                    ' new instance of StreamWriter called fileWrite
                    Dim fileWrite As New StreamWriter(file)
                    ' writes the text from the txtEditor text box to the text file
                    fileWrite.Write(txtEditor.Text)
                    ' closes fileWrite
                    fileWrite.Close()
                    ' sets the fileChanged variable to false
                    fileChanged = False
                    ' appends the file name to the form title
                    Me.Text = "Text Editor - " & path
                    ' sets the star variable to 0
                    star = 0
                End Using
            End If
        End If
    End Sub
#End Region
End Class