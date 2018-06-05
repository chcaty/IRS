<template>
    <div id="admin">
        <el-form id="toolbar" :inline="true" :model="searchForm" class="demo-form-inline">
            <el-form-item label="报修时间">
                <el-date-picker v-model="searchForm.Date" type="daterange" size="mini" start-placeholder="开始日期" end-placeholder="结束日期" value-format="yyyy-MM-dd" range-separator="至" :picker-options="pickerOptions2">
                </el-date-picker>
            </el-form-item>
            <el-form-item>
                <div style="margin-top: 2px">
                    <el-checkbox-group v-model="searchForm.CheckList" size="mini">
                        <el-checkbox v-for="li in list" :label="li" :key="li" border>{{li}}</el-checkbox>
                    </el-checkbox-group>
                </div>
            </el-form-item>
            <el-form-item>
                <el-button @click="find()" size="mini" plain>查询</el-button>
                <el-button type="info" size="mini" @click="findReset()" plain>重置</el-button>
            </el-form-item>
        </el-form>
        <el-row :gutter="10">
            <el-col :xs="24" :sm="24" :md="24" :lg="24">
                <div class="grid-content bg-purple" style="width: 800px; height:400px" id="dormmain"></div>
            </el-col>
            <el-col :xs="24" :sm="24" :md="24" :lg="24">
                <div class="grid-content bg-purple" style="width: 800px; height:400px" id="faultmain"></div>
            </el-col>
        </el-row>
        <el-row :gutter="10">
            <el-col :xs="24" :sm="24" :md="24" :lg="24">
                <div class="grid-content bg-purple" style="width: 800px; height:400px" id="solvermain"></div>
            </el-col>
            <el-col :xs="24" :sm="24" :md="24" :lg="24">
                <div class="grid-content bg-purple" style="width: 800px; height:400px" id="usermain"></div>
            </el-col>
        </el-row>
    </div>
</template>
<script>
    import {
        getInfo,
        SearchModel
    } from "@/api/chart";

    var echarts = require('echarts');
    const checkList = ['宿舍楼报修分析','处理情况分析','故障情况分析','维修员维修分析'];
    export default{
        data(){
            return {
                pickerOptions2: {
                    shortcuts: [{
                        text: '最近一周',
                        onClick(picker) {
                            const end = new Date();
                            const start = new Date();
                            start.setTime(start.getTime() - 3600 * 1000 * 24 * 7);
                            picker.$emit('pick', [start, end]);
                        }
                    }, {
                        text: '最近一个月',
                        onClick(picker) {
                            const end = new Date();
                            const start = new Date();
                            start.setTime(start.getTime() - 3600 * 1000 * 24 * 30);
                            picker.$emit('pick', [start, end]);
                        }
                    }, {
                        text: '最近三个月',
                        onClick(picker) {
                            const end = new Date();
                            const start = new Date();
                            start.setTime(start.getTime() - 3600 * 1000 * 24 * 90);
                            picker.$emit('pick', [start, end]);
                        }
                    }]
                },
                list:checkList,
                searchForm: new SearchModel(),
            }
        },
        methods:{
            find(){
                this.fetchData()
            },
            fetchData(searchObj = this.searchForm) {
                getInfo(searchObj).then(response => {
                    let result = response.data;
                    this.setDormOption(response.dormlist,response.timelist,response.dormcategory);
                    this.setFaultOption(response.faultlist,response.timelist,response.faultcategory);
                    this.setSolverOption(response.solverlist,response.timelist,response.solvercategory);
                })
                .catch(() => {
                    this.loading = false
                });
            },
            findReset() {
                this.searchForm = new SearchModel()
                this.fetchData()
            },
            setDormOption(Data,timelist,category){
                let dormChart = echarts.init(document.getElementById('dormmain'));
                dormChart.clear();
                dormChart.setOption({
                    title:{
                        text:'宿舍楼报修情况分析',
                        subtext:'报修数量'
                    },
                    tooltip : {
                        trigger: 'axis',
                    },
                    toolbox:{
                        show:true,
                        feature:{
                            magicType: {type: ['line', 'bar']},
                            saveAsImage:{
                                show:true
                            }
                        },
                    },
                    legend:{
                        data:category,
                    },
                    yAxis: {
                        type: 'value',
                        axisLabel: {
                            formatter: '{value} 个'
                        }
                    },
                    xAxis:[{
                        type: 'category',
                        boundaryGap: false,
                        data:timelist
                    }],
                    series:this.setSerie(Data)
                })
            },
            setFaultOption(Data,timelist,category){
                let dormChart = echarts.init(document.getElementById('faultmain'));
                dormChart.clear();
                dormChart.setOption({
                    title:{
                        text:'故障情况分析',
                        subtext:'报修数量'
                    },
                    tooltip : {
                        trigger: 'axis',
                    },
                    toolbox:{
                        show:true,
                        feature:{
                            magicType: {type: ['line', 'bar']},
                            saveAsImage:{
                                show:true
                            }
                        },
                    },
                    legend:{
                        data:category,
                    },
                    yAxis: {
                        type: 'value',
                        axisLabel: {
                            formatter: '{value} 个'
                        }
                    },
                    xAxis:[{
                        type: 'category',
                        boundaryGap: false,
                        data:timelist
                    }],
                    series:this.setSerie(Data)
                })
            },
            setSolverOption(Data,timelist,category){
                let dormChart = echarts.init(document.getElementById('solvermain'));
                dormChart.clear();
                dormChart.setOption({
                    title:{
                        text:'处理情况分析',
                        subtext:'报修数量'
                    },
                    tooltip : {
                        trigger: 'axis',
                    },
                    toolbox:{
                        show:true,
                        feature:{
                            magicType: {type: ['line', 'bar']},
                            saveAsImage:{
                                show:true
                            }
                        },
                    },
                    legend:{
                        data:category,
                    },
                    yAxis: {
                        type: 'value',
                        axisLabel: {
                            formatter: '{value} 个'
                        }
                    },
                    xAxis:[{
                        type: 'category',
                        boundaryGap: false,
                        data:timelist
                    }],
                    series:this.setSerie(Data)
                })
            },
            setUserOption(Data,timelist,category){
                let dormChart = echarts.init(document.getElementById('usermain'));
                dormChart.clear();
                dormChart.setOption({
                    title:{
                        text:'维修员维修情况分析',
                        subtext:'报修数量'
                    },
                    tooltip : {
                        trigger: 'axis',
                    },
                    toolbox:{
                        show:true,
                        feature:{
                            magicType: {type: ['line', 'bar']},
                            saveAsImage:{
                                show:true
                            }
                        },
                    },
                    legend:{
                        data:category,
                    },
                    yAxis: {
                        type: 'value',
                        axisLabel: {
                            formatter: '{value} 个'
                        }
                    },
                    xAxis:[{
                        type: 'category',
                        boundaryGap: false,
                        data:timelist
                    }],
                    series:this.setSerie(Data)
                })
            },
            setSerie(data){
                var serie = [];
                for(var i = 0; i < data.length; i++){
                    var item = {
                        name:data[i].dataName,
                        type: 'line',
                        data: data[i].dataList,
                        markPoint: {
                            data: [
                                {type: 'max', name: '最大值'},
                                {type: 'min', name: '最小值'}
                            ]
                        },
                        markLine: {
                            data: [
                                {type: 'average', name: '平均值'}
                            ]
                        }
                    }
                    serie.push(item );
                };
                return serie;
            }
        },
        mounted(){
        }
    }
</script>

<style lang="scss">
@import "./../../styles/app-main.scss";

.avatar-uploader .el-upload {
    border: 1px dashed #d9d9d9;
    border-radius: 6px;
    cursor: pointer;
    position: relative;
    overflow: hidden;
}

.avatar-uploader .el-upload:hover {
    border-color: #20a0ff;
}

.avatar-uploader-icon {
    font-size: 28px;
    color: #8c939d;
    width: 178px;
    height: 178px;
    line-height: 178px;
    text-align: center;
}
.avatar {
    width: 178px;
    height: 178px;
    display: block;
}

#admin .el-form--label-top .el-form-item__label {
    width: 100%;
    text-align: center;
}
#admin .el-form-item__content {
    text-align: center;
}
#admin .el-col-10 > .el-form-item > .el-form-item__content > .el-input {
    width: 80%;
}

#admin .first-row .el-col {
    border: 1px solid $border-color;
    border-left: 0px;
}
#admin .first-row .first-column {
    border-left: 1px solid $border-color;
}
#admin .normal-row .el-col {
    border: 1px solid $border-color;
    border-left: 0px;
    border-top: 0px;
}
#admin .normal-row .first-column {
    border-left: 1px solid $border-color;
}

#admin .last-row .el-col {
    border: 1px solid $border-color;
    border-top: 0px;
    border-left: 0px;
}
#admin .last-row .first-column {
    border-left: 1px solid $border-color;
}
#admin .last-row .first-column .el-upload-dragger {
    width: auto;
    height: auto;
}
</style>

