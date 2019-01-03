Feature: PerchauseProducts
	In order to avoid silly mistakes
	As a math idiot
	I want to be told the sum of two numbers

@mytag
Scenario Outline: Buy a product
	Given I have added item <item> into the Cart and I want this amount <amount>
	And The shop have product <product> in  stock<stock>
	When i press Perchause
	Then the result should be succes <succes>

	Examples: 
	| item | amount | product | stock | succes |
	| 1    | 1      | 1       | 1     | true   |
	| 1    | 1      | 1       | 0     | false  |
	| 1    | 2      | 1       | 1     | false  |
	| 1    | 1      | 1       | 5     | true   |
