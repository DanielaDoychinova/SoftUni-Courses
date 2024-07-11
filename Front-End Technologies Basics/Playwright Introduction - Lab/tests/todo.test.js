const { test, expect } = require('@playwright/test');

//verify user can add tasks 
test("User can add task", async ({page}) => {
    await page.goto('http://localhost:8080/');
    await page.fill('#task-input', 'Test Task 1');
    await page.click('#add-task');
    const taskText = await page.textContent('.task');
    expect(taskText).toContain('Test Task')
});



//verify user can delete task 
test("user can delete task", async ({page}) => {
    await page.goto('http://localhost:8080/');
    await page.fill('#task-input', "Test Task");
    await page.click('#add-task');
    await page.click('.task .delete-task');
    const tasks = await page.$$eval('.task', tasks => 
        tasks.map( task => task.textContent

        ));
        expect(tasks).not.toContain('Test Task');
});



//verify the user can mark a task as completed
test("User can mark a task as completed", async ({page}) => {
    await page.goto('http://localhost:8080/');
    await page.fill('#task-input', 'Test Task');
    await page.click('#add-task');

    await page.click('.task .task-complete');

    const completedTask = await page.$('.task.completed');

    expect(completedTask).not.toBeNull();
});


// //verify user can filter tasks
test("User can filter tasks", async ({page}) => {
    await page.goto('http://localhost:8080/');
    await page.fill('#task-input', "Test Task");
    await page.click('#add-task');

    await page.click('.task .task-complete');
    await page.selectOption('#filter', 'Completed');

    const incompleteTask = await page.$('.task:not(.completed)');
    expect(incompleteTask).toBeNull();
});



