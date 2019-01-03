describe('Shop functionality.', () => {

    beforeEach(() => {
        cy.visit("/shop");
    });

    it('Default shop page is showing all products.', () => {
        // Get the default amount of products in webshop.
        let defaultProductsCount = parseInt(Cypress.config('$defaultProductsCount'));
        
        cy.get('[data-cy=search-product]').should(($e) => {
            // Check if the number of products come in search is
            // equals to default number of products.
            expect($e).to.have.length(defaultProductsCount);
        });
    });

    it('Color type is visible on the products.', () => {
        cy.get('[data-cy=search-product]').as('search-product');
        // Check if color type is visible on all products.
        cy.get('[data-cy=search-product] [data-cy=search-product-color-display]').should('be.visible');
    });

    it('Price is visible on the products.', () => {
        cy.get('[data-cy=search-product]').as('search-product');
        // Check if price is visible on all products.
        cy.get('[data-cy=search-product] [data-cy=search-product-price-display]').should('be.visible');
    });

    
    it('Title is visible on the products.', () => {
        cy.get('[data-cy=search-product]').as('search-product');
        // Check if title is visible on all products.
        cy.get('[data-cy=search-product] [data-cy=search-product-title-display]').should('be.visible');
    });

    it('Image is visible on the products.', () => {
        cy.get('[data-cy=search-product]').as('search-product');
        // Check if image is visible on all products.
        cy.get('[data-cy=search-product] [data-cy=search-product-image-display]').should('be.visible');
    });
});