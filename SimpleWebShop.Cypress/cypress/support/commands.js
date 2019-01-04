// ***********************************************
// This example commands.js shows you how to
// create various custom commands and overwrite
// existing commands.
//
// For more comprehensive examples of custom
// commands please read more here:
// https://on.cypress.io/custom-commands
// ***********************************************
//
//
// -- This is a parent command --
// Cypress.Commands.add("login", (email, password) => { ... })
//
//
// -- This is a child command --
// Cypress.Commands.add("drag", { prevSubject: 'element'}, (subject, options) => { ... })
//
//
// -- This is a dual command --
// Cypress.Commands.add("dismiss", { prevSubject: 'optional'}, (subject, options) => { ... })
//
//
// -- This is will overwrite an existing command --
// Cypress.Commands.overwrite("visit", (originalFn, url, options) => { ... })

Cypress.Commands.add('move', { prevSubject: 'element'}, (subject, options) => { 
    // Invoke the jquery function offset for getting the
    // offset of the element in the DOM.
    cy.wrap(subject).invoke('offset').then(offset => {
        cy.wrap(subject)
            .trigger('mousedown', { which: 1, pageX: offset.left, pageY: offset.top })
            .trigger('mousemove', { which: 1, pageX:  offset.left + options.left, pageY:  offset.top })
            .trigger('mouseup');
    });
});