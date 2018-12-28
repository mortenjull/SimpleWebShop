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

    it('Returns products with higher price when applying min price filter.', () => {
        // Get the min price slider.
        cy.get('[data-cy=search-min-price-slider]').as('search-min-price-slider');
        
        // Get the offset of the min price rangler slider.
        cy.get('@search-min-price-slider').invoke('offset').then(minOffset => {
            // Move the min price range slider.
            cy.get('@search-min-price-slider')
                .trigger('mousedown', { which: 1, pageX: minOffset.left, pageY: minOffset.top })
                .trigger('mousemove', { which: 1, pageX:  minOffset.left + 50, pageY:  minOffset.top })
                .trigger('mouseup');

            // Get the selected min and max price.
            cy.get('[data-cy=search-min]').invoke('val').then($min => {
                cy.get('[data-cy=search-max]').invoke('val').then($max => {
                    // Convert string values to float.
                    $min = parseFloat($min);
                    $max = parseFloat($max);

                    // Apply default filter options.
                    cy.get('[data-cy=search-submit').click();
                    
                    // Check if each product has a price below the choosen min price.
                    cy.get('[data-cy=search-product-price]').invoke('val').should($price => {
                        expect(parseFloat($price)).to.be.within($min, $max);
                    });
                });
            });
        });
    });

    it('Returns products with lower price when applying max price filter.', () => {
        // Get the max price slider.
        cy.get('[data-cy=search-max-price-slider]').as('search-max-price-slider');
        
        // Get the offset of the min price rangler slider.
        cy.get('@search-max-price-slider').invoke('offset').then(maxOffset => {
            // Move the max price range slider.
            cy.get('@search-max-price-slider')
                .trigger('mousedown', { which: 1, pageX: maxOffset.left, pageY: maxOffset.top })
                .trigger('mousemove', { which: 1, pageX:  maxOffset.left - 50, pageY:  maxOffset.top })
                .trigger('mouseup');

            // Get the selected min and max price.
            cy.get('[data-cy=search-min]').invoke('val').then($min => {
                cy.get('[data-cy=search-max]').invoke('val').then($max => {
                    // Convert string values to float.
                    $min = parseFloat($min);
                    $max = parseFloat($max);

                    // Apply default filter options.
                    cy.get('[data-cy=search-submit').click();
                    
                    // Check if each product has a price below the choosen max price.
                    cy.get('[data-cy=search-product-price]').invoke('val').should($price => {
                        expect(parseFloat($price)).to.be.within($min, $max);
                    });
                });
            });
        });
    });

    it('Returns with price between min and max price filter.', () => {
        // Get the max price slider.
        cy.get('[data-cy=search-min-price-slider]').as('search-min-price-slider');
        cy.get('[data-cy=search-max-price-slider]').as('search-max-price-slider');
        
        // Get the offset of the min price rangler slider.
        cy.get('@search-max-price-slider').invoke('offset').then(maxOffset => {
            cy.get('@search-min-price-slider').invoke('offset').then(minOffset => {
                // Move the max price range slider.
                cy.get('@search-max-price-slider')
                    .trigger('mousedown', { which: 1, pageX: maxOffset.left, pageY: maxOffset.top })
                    .trigger('mousemove', { which: 1, pageX:  maxOffset.left - 50, pageY:  maxOffset.top })
                    .trigger('mouseup');

                // Move the min price range slider.
                cy.get('@search-min-price-slider')
                    .trigger('mousedown', { which: 1, pageX: minOffset.left, pageY: minOffset.top })
                    .trigger('mousemove', { which: 1, pageX:  minOffset.left + 50, pageY:  minOffset.top })
                    .trigger('mouseup');

                // Get the selected min and max price.
                cy.get('[data-cy=search-min]').invoke('val').then($min => {
                    cy.get('[data-cy=search-max]').invoke('val').then($max => {
                        // Convert string values to float.
                        $min = parseFloat($min);
                        $max = parseFloat($max);

                        // Apply default filter options.
                        cy.get('[data-cy=search-submit').click();
                        
                        // Check if each product has a price below the choosen max price.
                        cy.get('[data-cy=search-product-price]').invoke('val').should($price => {
                            expect(parseFloat($price)).to.be.within($min, $max);
                        });
                    });
                });
            });
        });
    });

});