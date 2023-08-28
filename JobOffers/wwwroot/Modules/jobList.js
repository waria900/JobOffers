// we give the ajax request on URL and will return as json format.

// This way is called:  ( Object Literals ).
var ClsJobs = {

    // This will show GetAllItemsData but you have to fix it.
    GetAllDataItems: function () {
        Helper.AjaxCallGet("https://localhost:7182/api/Jobs", {}, "json",
            function (Response) {

               // console.log(Response);

                var htmlData = "";
                for (var i = 0; i < Response.data.length; i++) {
                    htmlData += this.ClsJobs.DrwaAllDataItem(Response.data[i]);
                    //console.log(htmlData);
                }
                var d2 = document.getElementById('JobsArea');
                d2.innerHTML = htmlData;



            }, function () { }
        )
    },

    DrwaAllDataItem: function (item) {


        
        var data = "<div  class='single-job-items mb-30'><div class='job-items'>";
        data += "<div class='company-img'><a href='/JobDetails/VwJobDetails?JobId=" + item.jobId + "'><img src='/Uploads/Jobs/" + item.companyLogo + "' alt='' style='width:85px;height:86px'></a></div>";
        data += "<div class='job-tittle'><a href='/JobDetails/VwJobDetails?JobId=" + item.jobId + "'><h4>" + item.jobName + "</h4></a>";
        data += "<ul><li>" + item.companyName + "</li><li><i class='fas fa-map-marker-alt'></i>" + item.companyAddress + "</li><li>$" + item.minSalary + " - $" + item.maxSalary + "</li></ul></div></div>";
        data += "<div class='items-link f-right'><a href='/JobDetails/VwJobDetails?JobId=" + item.jobId + "'>" + item.jobTypeName + "</a><span>" + item.postedDate + "</span></div></div>";


        return data;

    }





}