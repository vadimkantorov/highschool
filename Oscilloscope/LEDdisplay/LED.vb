Public Class LED


    Private Sub LED_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        SetNumber(0)

    End Sub


    Public Sub SetNumber(ByVal num As Integer)

        Select Case num
            Case 0
                pbxLED.Image = My.Resources.zero
            Case 1
                pbxLED.Image = My.Resources.one
            Case 2
                pbxLED.Image = My.Resources.two
            Case 3
                pbxLED.Image = My.Resources.three
            Case 4
                pbxLED.Image = My.Resources.four
            Case 5
                pbxLED.Image = My.Resources.five
            Case 6
                pbxLED.Image = My.Resources.six
            Case 7
                pbxLED.Image = My.Resources.seven
            Case 8
                pbxLED.Image = My.Resources.eight
            Case 9
				pbxLED.Image = My.Resources.nine
			Case -1
				pbxLED.Image = My.Resources.one_with_comma
			Case 10
				pbxLED.Image = My.Resources.zero_with_comma
			Case 11
				pbxLED.Image = My.Resources.one_with_comma
			Case 12
				pbxLED.Image = My.Resources.two_with_comma
			Case 13
				pbxLED.Image = My.Resources.three_with_comma
			Case 14
				pbxLED.Image = My.Resources.five_with_comma
			Case 15
				pbxLED.Image = My.Resources.five_with_comma
			Case 16
				pbxLED.Image = My.Resources.six_with_comma
			Case 17
				pbxLED.Image = My.Resources.seven_with_comma
			Case 18
				pbxLED.Image = My.Resources.eight_with_comma
			Case 19
				pbxLED.Image = My.Resources.nine_with_comma
            Case Else
                pbxLED.Image = My.Resources.zero
        End Select

    End Sub


End Class
