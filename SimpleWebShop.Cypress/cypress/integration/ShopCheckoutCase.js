describe('As a user I want to press on button to add a item to my cart',function(){
	it('go to shop site', function(){
		cy.visit('http://localhost:52771/Shop')
	})
	it('pressing the add to cart button',function(){
		cy.visit('http://localhost:52771/Shop')
		cy.get('.add-to-cart-btn').first().click({ force: true })
	})
})