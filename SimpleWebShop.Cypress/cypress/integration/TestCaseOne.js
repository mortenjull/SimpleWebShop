describe('TestCaseOne',function(){
	it('SearchForSite', function(){
		cy.visit('https://simplewebshop-staging.azurewebsites.net/Shop')
	})
	it('SortByOptions', function(){
		cy.visit('https://simplewebshop-staging.azurewebsites.net/Shop')
		cy.get('.list').should('be.visible', false)
		cy.get('.nice-select').click()
		cy.get('.list').should('be.visible')
		cy.get('.list').get('.option').contains('Newest').click()
		cy.get('.current').should('have.text', 'Newest')
	})
})