# Issue-Demonstration-344421
https://developercommunity.visualstudio.com/content/problem/344421/wpf-bindinggroup-leaks-validation-results.html

1. Compile and run the application.
1. Enter a non-empty value into one of the cells and press enter to commit.
1. Edit the value multiple times, but don't clear it.
1. Click the explore button to see the list of validations against each row.
1. Clear the value (without deleting the row) and press enter to commit the value.
1. Note that the error marker is still visible in the row header.
1. Click the explore button again to see the list of validations against each row, note that they haven't gone away.
