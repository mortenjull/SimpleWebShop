describe('Shop Search functionality.', () => {

    beforeEach(() => {
        cy.visit("/shop");
    });

    it('Returns products sorted by highest price.', () => {
        // Sort by the highest price.
        search_sortby('2');
        
        // Do the search for products.
        search();

        // Last value which were checked.
        let last = null;

        // Loop over each product and check that the products are listed
        // with highest price first.
        cy.get('[data-cy=search-product-price]').each(($el, index, $list) => {
            let value = parseInt($el.val());
            if (last != null)
                expect(value).to.be.lessThan(last);
            last = value;
        });
    });

    it('Returns products sorted by lowest price.', () => {
        // Sortby the lowest price.
        search_sortby('1');

        // Do the search for products.
        search();

        // Last value which were checked.
        let last = null;

        // Loop over each product and check that the products are listed
        // with highest price first.
        cy.get('[data-cy=search-product-price]').each(($el, index, $list) => {
            let value = parseInt($el.val());
            if (last != null)
                expect(value).to.be.greaterThan(last);
            last = value;
        });
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

        // Do the search for products.
        search();
        
        cy.get('[data-cy=search-product]').should(($e) => {
            // Check if the number of products come in search is
            // equals to default number of products.
            expect($e).to.have.length(defaultProductsCount);
        });
    });

    it('Returns products with higher price when applying min price filter.', () => {
        search_move_range_slider(+50, 0);

        get_select_price_range((selectedMin, selectedMax) => {
            // Do the search for products.
            search();
                
            // Go over each product returned and check if the price is in the
            // given range.
            cy.get('[data-cy=search-product-price]').each(($el, index, $list) => {
                expect(parseFloat($el.val())).to.be.within(parseFloat(selectedMin), parseFloat(selectedMax));
            });
        });
    });

    it('Returns products with lower price when applying max price filter.', () => {
        // Move the max range slider.
        search_move_range_slider(0, -50);

        get_select_price_range((selectedMin, selectedMax) => {
            // Do the search for products.
            search();
                
            // Check if each product has a price below the choosen max price.
            cy.get('[data-cy=search-product-price]').each(($el, index, $list) => {
                expect(parseFloat($el.val())).to.be.within(parseFloat(selectedMin), parseFloat(selectedMax));
            });
        });
    });

    it('Returns with price between min and max price filter.', () => {
        // Move the range sliders.
        search_move_range_slider(+50, -50);

        get_select_price_range((selectedMin, selectedMax) => {
            // Do the search for products.
            search();
                
            // Check if each product has a price below the choosen max price.
            cy.get('[data-cy=search-product-price]').each(($el, index, $list) => {
                expect(parseFloat($el.val())).to.be.within(parseFloat(selectedMin), parseFloat(selectedMax));
            });
        });
    });

    it('Returns only products with selected color.', () => {
        // Get all search color checkboxes.
        cy.get('[data-cy=search-color]').as('search-color');
        // Un check all checkboxes.
        cy.get('@search-color').uncheck();
        // Get the first checkbox and check it.
        cy.get('@search-color').first().check();

        cy.get('@search-color').first().invoke('val').then($val => {
            // Do the search for products.
            search();

            cy.get('[data-cy=search-product-color]').each(($el, index, $list) => {
                expect(parseInt($el.val())).to.equal(parseInt($val));
            });
        });
    });

    it('Returns products with the applied price range and colors, and sortby selection.', () => {
        // Move the range sliders to apply price filter.
        search_move_range_slider(+50, -50);
        
        // Select the sorty by highest price.
        search_sortby('2');

        // Get all search color checkboxes.
        cy.get('[data-cy=search-color]').as('search-color');
        // Un check all checkboxes.
        cy.get('@search-color').uncheck();
        // Get the first checkbox and check it.
        cy.get('@search-color').first().check();

        // Get the id of the first color element.
        cy.get('@search-color').first().invoke('val').then($val => {
            
            // Get the selected price range.
            get_select_price_range((selectedMin, selectedMax) => {
                
                // Apply the selected search filters.
                search();

                // Check if each product has a price below the choosen max price.
                cy.get('[data-cy=search-product-price]').each(($el, index, $list) => {
                    expect(parseFloat($el.val())).to.be.within(parseFloat(selectedMin), parseFloat(selectedMax));
                });

                let last = null;

                // Validate that the products is sorted by highest price.
                cy.get('[data-cy=search-product-price]').each(($el, index, $list) => {
                    let value = parseInt($el.val());
                    if (last != null)
                        expect(value).to.be.lessThan(last);
                    last = value;
                });

                // Validate that all the products have the selected color.
                cy.get('[data-cy=search-product-color]').each(($el, index, $list) => {
                    expect(parseInt($el.val())).to.equal(parseInt($val));
                });
            });
        });
    });

    function search_move_range_slider(min, max) {
        // Get the max price slider.
        cy.get('[data-cy=search-min-price-slider]').as('search-min-price-slider');
        cy.get('[data-cy=search-max-price-slider]').as('search-max-price-slider');
        
        // Move the min and max range sliders.
        cy.get('@search-max-price-slider').move({ left: max});
        cy.get('@search-min-price-slider').move({ left: min});
    }

    function search_sortby(type) {
        cy.get('[data-cy=search-sortby]').as('search-sortby');
        cy.get('@search-sortby').select(type, { force: true });
    }

    function search() {
        cy.get('[data-cy=search-submit').click();
    }

    function get_select_price_range(callback) {
        // Get the selected min and max price.
        cy.get('[data-cy=search-min]').invoke('val').then($min => {
            cy.get('[data-cy=search-max]').invoke('val').then($max => {
                callback($min, $max);
            });
        });
    }
});