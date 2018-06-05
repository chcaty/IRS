<template>
    <div>
        <el-row :gutter="20">
            <el-col :span="24">
                <el-row :gutter="20" class="mgb20">
                    <el-col :span="8">
                        <el-card shadow="hover" :body-style="{padding: '0px'}">
                            <div class="grid-content grid-con-1">
                                <i class="el-icon-tickets grid-con-icon"></i>
                                <div class="grid-cont-right">
                                    <div class="grid-num">{{todayCount}}</div>
                                    <div>今日报修数量</div>
                                </div>
                            </div>
                        </el-card>
                    </el-col>
                    <el-col :span="8">
                        <el-card shadow="hover" :body-style="{padding: '0px'}">
                            <div class="grid-content grid-con-2">
                                <i class="el-icon-date grid-con-icon"></i>
                                <div class="grid-cont-right">
                                    <div class="grid-num">{{weekCount}}</div>
                                    <div>最近一周报修数量</div>
                                </div>
                            </div>
                        </el-card>
                    </el-col>
                    <el-col :span="8">
                        <el-card shadow="hover" :body-style="{padding: '0px'}">
                            <div class="grid-content grid-con-3">
                                <i class="el-icon-time grid-con-icon"></i>
                                <div class="grid-cont-right">
                                    <div class="grid-num">{{allCount}}</div>
                                    <div>总报修数量</div>
                                </div>
                            </div>
                        </el-card>
                    </el-col>
                </el-row>
                <el-card shadow="hover" :body-style="{ height: '304px'}">
                    <div slot="header" class="clearfix">
                        <span>最新的报修</span>
                        <el-button style="float: right; padding: 3px 0" @click="reload()" type="text">刷新</el-button>
                    </div>
                    <el-table :data="tableData" :border="true" style="width: 100%" v-loading="loading">
                        <el-table-column prop="recordId" label="序号" v-if="false" width="100">
                        </el-table-column>
                        <el-table-column prop="recordUser" label="报修人" width="100">
                        </el-table-column>
                        <el-table-column prop="userPhone" label="联系电话" width="150">
                        </el-table-column>
                        <el-table-column prop="dormCategoryName" label="宿舍楼" width="100">
                        </el-table-column>
                        <el-table-column prop="dormCategory" label="宿舍楼Id" v-if="false" width="80">
                        </el-table-column>
                        <el-table-column prop="userDorm" label="宿舍号" width="90">
                        </el-table-column>
                        <el-table-column prop="faultCategoryName" label="故障分类" width="100">
                        </el-table-column>
                        <el-table-column prop="faultCategory" label="故障分类Id" v-if="false" width="100">
                        </el-table-column>
                        <el-table-column prop="faultDesc" label="故障描述" width="180">
                        </el-table-column>
                        <el-table-column prop="recordTime" label="报修时间" width="150">
                        </el-table-column>
                        <el-table-column prop="faultResultName" label="处理情况" width="100%">
                        </el-table-column>
                        <el-table-column prop="faultResult" label="处理情况Id" v-if="false" width="100">
                        </el-table-column>
                    </el-table>
                </el-card>

            </el-col>
        </el-row>
    </div>
</template>

<script>
import {
  getCount,
  getTodaylist
} from "@/api/dashboard";
    export default {
        data() {
            return {
                //name: localStorage.getItem('ms_username'),
                tableData:[],
                todayCount:"",
                weekCount:"",
                allCount:"",
                loading: false,
            }
        },
        method: {
            reload(){
                this.loading = true
                getTodaylist().then(res =>{
                    this.tableData = res.data;
                    this.loading = false;
                });
            }
        },
        beforeCreate() {
            getCount().then(res =>{
                this.todayCount = res.todaycount;
                this.weekCount = res.weekcount;
                this.allCount = res.allcount
            });
            getTodaylist().then(res =>{
                this.tableData = res.data
            });
        }
    }

</script>


<style scoped>
.el-row {
    margin-bottom: 20px;
}

.grid-content {
    display: flex;
    align-items: center;
    height: 100px;
}

.grid-cont-right {
    flex: 1;
    text-align: center;
    font-size: 12px;
    color: #999;
}

.grid-num {
    font-size: 30px;
    font-weight: bold;
}

.grid-con-icon {
    font-size: 50px;
    width: 100px;
    height: 100px;
    text-align: center;
    line-height: 100px;
    color: #fff;
}

.grid-con-1 .grid-con-icon {
    background: rgb(45, 140, 240);
}

.grid-con-1 .grid-num {
    color: rgb(45, 140, 240);
}

.grid-con-2 .grid-con-icon {
    background: rgb(100, 213, 114);
}

.grid-con-2 .grid-num {
    color: rgb(45, 140, 240);
}

.grid-con-3 .grid-con-icon {
    background: rgb(242, 94, 67);
}

.grid-con-3 .grid-num {
    color: rgb(242, 94, 67);
}

.user-info {
    display: flex;
    align-items: center;
    padding-bottom: 20px;
    border-bottom: 2px solid #ccc;
    margin-bottom: 20px;
}

.user-avator {
    width: 120px;
    height: 120px;
    border-radius: 50%;
}

.user-info-cont {
    padding-left: 50px;
    flex: 1;
    font-size: 14px;
    color: #999;
}

.user-info-cont div:first-child {
    font-size: 30px;
    color: #222;
}

.user-info-list {
    font-size: 14px;
    color: #999;
    line-height: 25px;
}

.user-info-list span {
    margin-left: 70px;
}

.mgb20 {
    margin-bottom: 20px;
}

.todo-item {
    font-size: 14px;
}

.todo-item-del {
    text-decoration: line-through;
    color: #999;
}
</style>
