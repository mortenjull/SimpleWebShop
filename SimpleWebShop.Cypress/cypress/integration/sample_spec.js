describe('my first test', function() {
	it('dont do much', function(){
		 cy.visit('https://simplewebshop-staging.azurewebsites.net/')
		expect(true).to.equal(true)
	})
	it('looks after stuff', function() {
		cy.visit('https://simplewebshop-staging.azurewebsites.net/')
		cy.contains('Shop').click()
		cy.url().should('include','/shop')
		
		cy.get('.range-price').
		should('be.visible')
	})
	it('strated from the top now we are here', function() {
		cy.visit('https://simplewebshop-staging.azurewebsites.net/')
		cy.contains('Shop').click()
		
		cy.url().should('include','/shop')
		
		cy.contains('Home').click()
		
		expect(cy.url()).to.not.equal(cy.url() + '/shop')
	})
})