describe('Navigation functionality.', () => {

    beforeEach(() => {
        cy.visit("/");
    });

    it('Can visit frontpage.', () => {
        // Get the base url for the application.
        let baseUrl = Cypress.config('baseUrl');
        // Navigate to homepage.
        cy.get('[data-cy=navigation-home]').click();
        // Check if url matches.
        cy.url().should('equal', baseUrl + '/');
    });

    it('Can visit shop page.', () => {
        // Get the base url for the application.
        let baseUrl = Cypress.config('baseUrl');
        // Navigate to homepage.
        cy.get('[data-cy=navigation-shop]').click();
        // Check if url matches.
        cy.url().should('equal', baseUrl + '/shop');
    });
});