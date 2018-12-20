Feature: NumberInRange
	In order to avoid silly mistakes
	As a math idiot
	I want to be told the sum of two numbers

Scenario Outline: Add two numbers
	Given I have entered <numberOne> into the calculator
	And I have entered <numberTwo> into the calculator
	When I press add
	Then the result should be <result> on the screen

	Examples:
	| numberOne | numberTwo | result |
	|50 |70 |120  |