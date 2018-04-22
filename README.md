# GeekHunter
Geek Registration System
GeekHunter project from Gemmy Phan has been done.
I 
Using GRS a recruitment agent should be able to:
- Register a new candidate:
+ First name / Last name
+ Select technologies candidate has experience in from the predefined list
- View all candidates
- Filter candidates by technology
- Delete candidates.
- Update candidates.

* To run the WPF application -
+	navigate to C:\Users\user\source\repos\GeekHunter\GeekHunter\GeekHunter\bin\Debug
+	execute GeekHunterUI.exe

SearchForm will be opened.

* To register a new candidate:
+	Click Create New button in the SearchForm
+ An InputForm  will pop up.
+	Enter a first name and last name
+ Click Add (>>) button or Remove (<<) button to choose one or more skill from list Box Skill to Selected Skill
+	Click Save

* To retrieve all candidates
+ Click back button to return SearchForm
+	Click Search button (Selected skill box left blank)

* To retrieve candidates that match one or more skills
+ Click Add (>>) button or Remove (<<) button to choose one or more skill from list Box Skill to Selected Skill
+	Click Search

* To delete candidates
+ Select a candidate in the bottom List View Box that you want to delete.
+	Click Delete button.

* To update candidates
+	Click Update button in the SearchForm
+ An InputForm  will pop up.
+	Enter a new first name or/and last name
+ The right hand box will show the skills that this candidate is having.
+ Click Add (>>) button or Remove (<<) button to choose one or more skill from the "Skill" box to "Selected Skill" if you want to change the Skills.
+	Click Save


* Technologies:
+	This appliocation was made by using C# (Windows Form) and SQLite
