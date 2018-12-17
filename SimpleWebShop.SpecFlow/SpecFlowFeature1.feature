Feature: NUmberInRangeTest
	In order to avoid silly mistakes
	As a math idiot
	I want to be told the sum of two numbers

@mytag
Scenario: Add two numbers
	Given I have entered <Number> into the test
	When I press add
	Then the result should be <Result> 

	Examples: 
	| Number | Result |
	| -1     | false  |
	| 0      | true   |
	| 50     | true   |
	| 100    | true   |
	| 101    | false  |
