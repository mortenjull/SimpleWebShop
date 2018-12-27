describe('Shop Search functionality.', () => {

    beforeEach(() => {
        cy.visit("/shop");
    });

    it('Returns all products when viewing page with no filters.', () => {
        // Get the default amount of products in webshop.
        let defaultProductsCount = parseInt(Cypress.config('$defaultProductsCount'));
        
        cy.get('[data-cy=search-product]').should(($e) => {
            // Check if the number of products come in search is
            // equals to default number of products.
            expect($e).to.have.length(defaultProductsCount);
        });
    });

    it('Returns all products when applying default filter options.', () => {
        // Get the default amount of products in webshop.
        let defaultProductsCount = parseInt(Cypress.config('$defaultProductsCount'));

        // Apply default filter options.
        cy.get('[data-cy=search-submit').click();
        
        cy.get('[data-cy=search-product]').should(($e) => {
            // Check if the number of products come in search is
            // equals to default number of products.
            expect($e).to.have.length(defaultProductsCount);
        });
    });

    it('Returns less elements when applying min price filter.', () => {
        cy.get('[data-cy=search-min-price-slider]').as('search-min-price-slider');
        
        let offset = cy.get('@search-min-price-slider').invoke("offset");
        console.log(offset);
        cy.get('@search-min-price-slider')
            .trigger('mousedown', { which: 1, pageX: offset.left, pageY: offset.top })
            .trigger('mousemove', { which: 1, pageX:  offset.left + 10, pageY:  offset.left + 10 })
            .trigger('mouseup')
    });

});