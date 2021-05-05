// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

//TEST DELETING AREA

const $area = document.querySelector("#viewTestButtonsArea");

$area.addEventListener("click", async event => {
    if(event.target.classList.contains("delete-test")) {
        event.preventDefault();
        let testId = event.target.getAttribute("data-Id");
        await fetch(`/Tests/Delete/${testId}`, { method: "DELETE" });
        window.location.reload()
    }
}); 
